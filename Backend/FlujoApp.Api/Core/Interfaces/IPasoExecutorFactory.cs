using FlujoApp.Api.Core.Entities;
using System.Threading.Tasks;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IPasoExecutorFactory
    {
        Task<Dictionary<string, object>> EjecutarPasoAsync(Paso paso, Dictionary<string, object> inputData);
    }
}

