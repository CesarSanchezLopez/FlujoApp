
using Microsoft.EntityFrameworkCore;

using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;

namespace FlujoApp.Api.Infraestructure.Repositories
{

    public class FlujoRepository : IFlujoRepository
    {
        private readonly ApplicationDbContext _context;

        public FlujoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Flujo> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Flujos
                .Include(f => f.Pasos)
                    .ThenInclude(p => p.Campos)
                .Include(f => f.Pasos)
                    .ThenInclude(p => p.Dependencias)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AgregarAsync(Flujo flujo)
        {
            await _context.Flujos.AddAsync(flujo);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
