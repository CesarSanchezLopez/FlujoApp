using FlujoApp.Api.Core.Entities;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IPasoExecutor
    {
        bool CanHandle(string tipo); // Para que la fábrica sepa si puede usar este ejecutor
        Task EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada);
    }
}
