using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;
using FlujoApp.Api.Dtos;
using FlujoApp.Api.Helpers;

namespace FlujoApp.Api.Core.Services
{
    public class FlujoService : IFlujoService
    {
        private readonly IFlujoRepository _repositorio;
        private readonly PasoExecutorFactory _factory;

        public FlujoService(IFlujoRepository repositorio, PasoExecutorFactory factory)
        {
            _repositorio = repositorio;
            _factory = factory;
        }

        public async Task EjecutarFlujoAsync(Guid flujoId, Dictionary<string, object> inputData)
        {
            var flujo = await _repositorio.ObtenerPorIdAsync(flujoId);
            if (flujo == null)
                throw new Exception("Flujo no encontrado");

            var pasosOrdenados = FlujoExecutionHelper.OrdenarPasos(flujo.Pasos.ToList());

            foreach (var nivel in pasosOrdenados)
            {
                var tareas = nivel.Select(async paso =>
                {
                    var executor = _factory.ObtenerExecutor(paso.Tipo);
                    await executor.EjecutarAsync(paso, inputData);
                });

                await Task.WhenAll(tareas);
            }
        }

        public async Task<Guid> CrearFlujoAsync(FlujoDto dto)
        {
            var flujo = new Flujo
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Pasos = dto.Pasos.Select(p => new Paso
                {
                    Id = Guid.NewGuid(),
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Tipo = p.Tipo,
                    Orden = p.Orden,
                    Campos = p.Campos.Select(c => new Campo
                    {
                        Id = Guid.NewGuid(),
                        Nombre = c.Nombre,
                        Tipo = c.Tipo,
                        Requerido = c.Requerido
                    }).ToList(),
                    Dependencias = p.Dependencias.Select(d => new PasoDependencia
                    {
                        Id = Guid.NewGuid(),
                        DependeDePasoId = d
                    }).ToList()
                }).ToList()
            };

            await _repositorio.AgregarAsync(flujo);
            await _repositorio.GuardarCambiosAsync();
            return flujo.Id;
        }

        public async Task<FlujoDto> ObtenerFlujoAsync(Guid flujoId)
        {
            var flujo = await _repositorio.ObtenerPorIdAsync(flujoId);
            // Mapear a DTO si es necesario
            return new FlujoDto
            {
                Nombre = flujo.Nombre,
                Descripcion = flujo.Descripcion,
                Pasos = flujo.Pasos.Select(p => new PasoDto
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Tipo = p.Tipo,
                    Orden = p.Orden,
                    Campos = p.Campos.Select(c => new CampoDto
                    {
                        Nombre = c.Nombre,
                        Tipo = c.Tipo,
                        Requerido = c.Requerido
                    }).ToList(),
                    Dependencias = p.Dependencias.Select(d => d.DependeDePasoId).ToList()
                }).ToList()
            };
        }
    }
}
