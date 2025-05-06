using FlujoApp.Api.Core.Entities;

namespace FlujoApp.Api.Core.Interfaces
{
    public interface IDatoUsuarioRepository
    {
        Task<List<DatoUsuario>> ObtenerPorFlujoYCamposAsync(Guid flujoId, List<string> campoCodigos);

        Task<List<DatoUsuario>> ObtenerPorFlujoIdAsync(Guid flujoId);
        Task GuardarAsync(DatoUsuario dato);
    }
}
