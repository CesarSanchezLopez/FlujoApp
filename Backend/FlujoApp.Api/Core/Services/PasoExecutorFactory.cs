using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;
using FlujoApp.Api.Core.Services.Ejecutores;

namespace FlujoApp.Api.Core.Services
{
    public class PasoExecutorFactory : IPasoExecutorFactory
    {
        private readonly IEnumerable<IPasoExecutor> _executors;

        public PasoExecutorFactory(IEnumerable<IPasoExecutor> executors)
        {
            _executors = executors;
        }

        // Método modificado para que devuelva un Dictionary<string, object> con los resultados
        public async Task<Dictionary<string, object>> EjecutarPasoAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            // Buscar un executor que pueda manejar el tipo de paso
            var executor = _executors.FirstOrDefault(e => e.CanHandle(paso.Tipo));
            if (executor == null)
                throw new InvalidOperationException($"No hay ejecutor para el tipo: {paso.Tipo}");

            // Ejecutar el paso usando el executor adecuado
            return await executor.EjecutarAsync(paso, datosEntrada);
        }
    }
}
