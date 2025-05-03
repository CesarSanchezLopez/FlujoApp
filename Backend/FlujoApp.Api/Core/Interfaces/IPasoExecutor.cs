using FlujoApp.Api.Core.Entities;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IPasoExecutor
    {
        Task EjecutarAsync(Paso paso, Dictionary<string, object> datosEntrada);
    }
}
