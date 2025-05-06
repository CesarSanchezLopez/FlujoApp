using Microsoft.AspNetCore.Mvc;
using FlujoApp.Api.Core.Interfaces;
using FlujoApp.Api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class FlujoController : ControllerBase
{
    private readonly IFlujoService _flujoService;

    public FlujoController(IFlujoService flujoService)
    {
        _flujoService = flujoService;
    }

    // POST: api/flujo
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] FlujoDto flujoDto)
    {
        var id = await _flujoService.CrearFlujoAsync(flujoDto);
        return Ok(id);
    }

    // GET: api/flujo/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<FlujoDto>> ObtenerFlujo(Guid id)
    {
        var flujo = await _flujoService.ObtenerFlujoAsync(id);
        if (flujo == null)
            return NotFound();

        return Ok(flujo);
    }

    // POST: api/flujo/{id}/ejecutar
    [HttpPost("{id}/ejecutar")]
    public async Task<IActionResult> EjecutarFlujo(Guid id, [FromBody] Dictionary<string, object> inputData)
    {
        try
        {
            var (resultadoFinal, mensajes) = await _flujoService.EjecutarFlujoAsync(id, inputData);

            return Ok(new
            {
                mensaje = "Flujo ejecutado correctamente",
                resultado = resultadoFinal,
                logs = mensajes.Select((msg, i) => new { paso = i + 1, mensaje = msg })
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}