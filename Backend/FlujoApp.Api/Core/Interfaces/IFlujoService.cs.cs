using FlujoApp.Api.Dtos;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IFlujoService
    {
        //Task<Dictionary<string, object>> EjecutarFlujoAsync(Guid flujoId, Dictionary<string, object> inputData);
        Task<(Dictionary<string, object> resultadoFinal, List<string> mensajes)> EjecutarFlujoAsync(Guid flujoId, Dictionary<string, object> inputData);
        Task<Guid> CrearFlujoAsync(FlujoDto dto);
        Task<FlujoDto> ObtenerFlujoAsync(Guid flujoId);
    }
}
