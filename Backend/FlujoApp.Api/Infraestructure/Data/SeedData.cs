using FlujoApp.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace FlujoApp.Api.Infraestructure.Data
{
    public static class SeedData
    {
        public static async Task InicializarAsync(ApplicationDbContext context)
        {
            if (await context.CampoCatalogos.AnyAsync())
                return; // Ya hay datos

            var campos = new List<CampoCatalogo>
        {
            new() { Codigo = "F-0001", Nombre = "Primer nombre", Tipo = "Texto" },
            new() { Codigo = "F-0002", Nombre = "Segundo nombre", Tipo = "Texto" },
            new() { Codigo = "F-0003", Nombre = "Primer apellido", Tipo = "Texto" },
            new() { Codigo = "F-0004", Nombre = "Segundo apellido", Tipo = "Texto" },
            new() { Codigo = "F-0005", Nombre = "Tipo de documento", Tipo = "Texto" },
            new() { Codigo = "F-0006", Nombre = "Número de documento", Tipo = "Texto" },
            new() { Codigo = "F-0007", Nombre = "Correo electrónico", Tipo = "Texto" }
        };

            context.CampoCatalogos.AddRange(campos);
            await context.SaveChangesAsync();
        }
    }
}
