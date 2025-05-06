using FlujoApp.Api.Core.Entities;
using FlujoApp.Api.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlujoApp.Api.Infraestructure.Repositories
{
    public class CampoCatalogoRepository : ICampoCatalogoRepository
    {
        private readonly ApplicationDbContext _context;

        public CampoCatalogoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CampoCatalogo>> ObtenerPorCodigosAsync(List<string> codigos)
        {
            return await _context.Set<CampoCatalogo>()
                .Where(c => codigos.Contains(c.Codigo))
                .ToListAsync();
        }
    }
}
