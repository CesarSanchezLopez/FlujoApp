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
        private readonly IDatoUsuarioRepository _datoUsuarioRepo;
        private readonly ICampoCatalogoRepository _catalogoRepo;

        public FlujoService(
            IFlujoRepository repositorio,
            IPasoExecutorFactory factory,
            IDatoUsuarioRepository datoUsuarioRepo, 
            ICampoCatalogoRepository catalogoRepo)
        {
            _repositorio = repositorio;
            _factory = factory;
            _datoUsuarioRepo = datoUsuarioRepo;
            _catalogoRepo = catalogoRepo;
        }

        public async Task<Dictionary<string, object>> EjecutarFlujoAsync(Guid flujoId, Dictionary<string, object> inputData)
        {
            var flujo = await _repositorio.ObtenerPorIdAsync(flujoId);
            if (flujo == null)
                throw new Exception("Flujo no encontrado");

            var pasosOrdenados = FlujoExecutionHelper.OrdenarPasos(flujo.Pasos.ToList());

            // Cargar datos previamente guardados del usuario para este flujo
            var datosGuardados = await _datoUsuarioRepo.ObtenerPorFlujoIdAsync(flujoId);
            var datosDisponibles = new Dictionary<string, object>(inputData);

            foreach (var dato in datosGuardados)
            {
                if (!datosDisponibles.ContainsKey(dato.CampoCodigo))
                {
                    datosDisponibles[dato.CampoCodigo] = dato.Valor;
                }
            }

            foreach (var nivel in pasosOrdenados)
            {
                var tareas = nivel.Select(async paso =>
                {
                    // 🔽 Asegurar que CampoCatalogo esté cargado
                    foreach (var campo in paso.Campos)
                    {
                        if (campo.CampoCatalogo == null)
                            throw new Exception($"Campo '{campo.CampoCodigo}' no tiene CampoCatalogo cargado.");
                    }

                  
                    var camposEntrada = paso.Campos
                        .Where(c => !string.IsNullOrEmpty(c.CampoCodigo)) // ignora CampoCatalogo
                        .Select(c => c.CampoCodigo)
                        .ToList();

                    var faltantes = camposEntrada
                        .Where(c => !datosDisponibles.ContainsKey(c))
                        .ToList();

                    if (faltantes.Any())
                        throw new Exception($"Faltan campos requeridos para el paso '{paso.Nombre}': {string.Join(", ", faltantes)}");

                    var entradaPaso = camposEntrada.ToDictionary(c => c, c => datosDisponibles[c]);

                    var resultado = await _factory.EjecutarPasoAsync(paso, entradaPaso);

                    foreach (var (campoCodigo, valor) in resultado)
                    {
                        datosDisponibles[campoCodigo] = valor;

                        await _datoUsuarioRepo.GuardarAsync(new DatoUsuario
                        {
                            Id = Guid.NewGuid(),
                            FlujoId = flujoId,
                            PasoId = paso.Id,
                            CampoCodigo = campoCodigo,
                            Valor = valor?.ToString() ?? ""
                        });
                    }
                });

                await Task.WhenAll(tareas);
            }
            return datosDisponibles;
        }

        public async Task<Guid> CrearFlujoAsync(FlujoDto dto)
        {
            var flujo = new Flujo
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            // 1. Obtener todos los códigos únicos de campo usados en el DTO
            var codigosCampo = dto.Pasos
                .SelectMany(p => p.Campos)
                .Select(c => c.Codigo)
                .Distinct()
                .ToList();

            // 2. Cargar los CampoCatalogo correspondientes desde base de datos
            var catalogos = await _catalogoRepo.ObtenerPorCodigosAsync(codigosCampo);
            var catalogoDict = catalogos.ToDictionary(c => c.Codigo, c => c);

            // 3. Crear pasos y campos con asociación a CampoCatalogoId
            var pasos = dto.Pasos.Select(p => new Paso
            {
                Id = Guid.NewGuid(),
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Tipo = p.Tipo,
                Orden = p.Orden,
                Campos = p.Campos.Select(c =>
                {
                    if (!catalogoDict.TryGetValue(c.Codigo, out var catalogo))
                        throw new Exception($"Campo código '{c.Codigo}' no existe en el catálogo");

                    return new Campo
                    {
                        CampoCodigo = c.Codigo,
                        CampoCatalogoId = catalogo.Id,
                        EsEntrada = true
                    };
                }).ToList(),
                Dependencias = new List<PasoDependencia>() // se llena luego
            }).ToList();

            // 4. Mapear dependencias
            var codigoToId = pasos.ToDictionary(p => p.Codigo, p => p.Id);
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
                    }).ToList();
            }

            flujo.Pasos = pasos;

            await _repositorio.AgregarAsync(flujo);
            await _repositorio.GuardarCambiosAsync();

            return flujo.Id;
        }

        public async Task<FlujoDto> ObtenerFlujoAsync(Guid flujoId)
        {
            // Obtener el flujo
            var flujo = await _repositorio.ObtenerPorIdAsync(flujoId);
            if (flujo == null) throw new Exception("Flujo no encontrado");

            // Diccionario para mapear Id → Codigo
            var idToCodigo = flujo.Pasos.ToDictionary(p => p.Id, p => p.Codigo);

            // Retornar el DTO del flujo
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
                        Codigo = c.CampoCodigo,
                        Nombre = c.CampoCatalogo.Nombre,
                        Tipo = c.CampoCatalogo.Tipo,
                        Requerido = c.CampoCatalogo.Requerido
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

