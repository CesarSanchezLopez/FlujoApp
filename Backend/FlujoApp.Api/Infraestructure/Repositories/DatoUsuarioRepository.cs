using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlujoApp.Api.Infraestructure.Repositories
{
    public class DatoUsuarioRepository : IDatoUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public DatoUsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DatoUsuario>> ObtenerPorFlujoYCamposAsync(Guid flujoId, List<string> campoCodigos)
        {
            return await _context.DatoUsuarios
                .Where(d => d.FlujoId == flujoId && campoCodigos.Contains(d.CampoCodigo))
                .ToListAsync();
        }
        public async Task<List<DatoUsuario>> ObtenerPorFlujoIdAsync(Guid flujoId)
        {
            return await _context.DatoUsuarios
                .Where(d => d.FlujoId == flujoId)
                .ToListAsync();
        }
        public async Task GuardarAsync(DatoUsuario dato)
        {
            _context.DatoUsuarios.Add(dato);
            await _context.SaveChangesAsync();
        }
    }
}
