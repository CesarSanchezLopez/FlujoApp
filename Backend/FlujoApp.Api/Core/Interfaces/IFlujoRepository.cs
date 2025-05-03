using FlujoApp.Api.Core.Entities;
namespace FlujoApp.Api.Core.Interfaces
{
    public interface IFlujoRepository
    {
        Task<Flujo> ObtenerPorIdAsync(Guid id);
        Task AgregarAsync(Flujo flujo);
        Task GuardarCambiosAsync();
    }
}
