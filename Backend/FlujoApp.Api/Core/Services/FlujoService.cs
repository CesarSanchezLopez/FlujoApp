using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;
using FlujoApp.Api.Dtos;
using FlujoApp.Api.Helpers;

namespace FlujoApp.Api.Core.Services
{
    public class FlujoService : IFlujoService
    {

        private readonly IFlujoRepository _repositorio;
        private readonly IPasoExecutorFactory _factory;
        public FlujoService(IFlujoRepository repositorio, IPasoExecutorFactory factory)
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
                var tareas = nivel.Select(paso =>
                    _factory.EjecutarPasoAsync(paso, inputData)
                );

                await Task.WhenAll(tareas);
            }
        }

        public async Task<Guid> CrearFlujoAsync(FlujoDto dto)
        {
            var flujo = new Flujo
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            // 1. Crear pasos con Ids y mantener un diccionario
            var pasos = dto.Pasos.Select(p => new Paso
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
                Dependencias = new List<PasoDependencia>() // se llenará después
            }).ToList();

            // 2. Crear diccionario Codigo → Id
            var codigoToId = pasos.ToDictionary(p => p.Codigo, p => p.Id);

            // 3. Asignar dependencias ahora que tenemos el diccionario
            foreach (var paso in pasos)
            {
                var pasoDto = dto.Pasos.First(p => p.Codigo == paso.Codigo);
                paso.Dependencias = pasoDto.Dependencias
                    .Select(depCodigo =>
                    {
                        if (!codigoToId.ContainsKey(depCodigo))
                            throw new Exception($"Dependencia '{depCodigo}' no encontrada para paso '{paso.Codigo}'");

                        return new PasoDependencia
                        {
                            Id = Guid.NewGuid(),
                            DependeDePasoId = codigoToId[depCodigo]
                        };
                    })
                    .ToList();
            }

            flujo.Pasos = pasos;

            await _repositorio.AgregarAsync(flujo);
            await _repositorio.GuardarCambiosAsync();
            return flujo.Id;
        }

        public async Task<FlujoDto> ObtenerFlujoAsync(Guid flujoId)
        {
            var flujo = await _repositorio.ObtenerPorIdAsync(flujoId);

            // Diccionario para mapear Id → Codigo
            var idToCodigo = flujo.Pasos.ToDictionary(p => p.Id, p => p.Codigo);

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
                    Dependencias = p.Dependencias
                        .Select(d => idToCodigo.ContainsKey(d.DependeDePasoId)
                            ? idToCodigo[d.DependeDePasoId]
                            : null)
                        .Where(c => c != null)
                        .ToList()
                }).ToList()
            };
        }
    }
}
