using FlujoApp.Api.Dtos;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IFlujoService
    {
        Task EjecutarFlujoAsync(Guid flujoId, Dictionary<string, object> inputData);
        Task<Guid> CrearFlujoAsync(FlujoDto dto);
        Task<FlujoDto> ObtenerFlujoAsync(Guid flujoId);
    }
}
