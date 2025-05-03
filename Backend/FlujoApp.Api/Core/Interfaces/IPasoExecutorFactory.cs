using FlujoApp.Api.Core.Entities;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IPasoExecutorFactory
    {
        Task EjecutarPasoAsync(Paso paso, Dictionary<string, object> inputData);
    }
}
