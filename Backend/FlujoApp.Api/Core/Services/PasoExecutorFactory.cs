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

        public async Task EjecutarPasoAsync(Paso paso, Dictionary<string, object> datosEntrada)
        {
            var executor = _executors.FirstOrDefault(e => e.CanHandle(paso.Tipo));
            if (executor == null)
                throw new InvalidOperationException($"No hay ejecutor para el tipo: {paso.Tipo}");

            await executor.EjecutarAsync(paso, datosEntrada);
        }
    }
}
