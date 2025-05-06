using FlujoApp.Api.Core.Entities;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IPasoExecutor
    {
        Task<Dictionary<string, object>> EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada);
        bool CanHandle(string tipoPaso);
    }
}
