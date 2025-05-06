using FlujoApp.Api.Core.Entities;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface ICampoCatalogoRepository
    {
        Task<List<CampoCatalogo>> ObtenerPorCodigosAsync(List<string> codigos);
    }
}
