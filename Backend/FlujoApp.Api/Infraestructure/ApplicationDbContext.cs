using FlujoApp.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlujoApp.Api.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Flujo> Flujos { get; set; }
        public DbSet<Paso> Pasos { get; set; }
        public DbSet<Campo> Campos { get; set; }
        public DbSet<CampoCatalogo> CampoCatalogos { get; set; }

        public DbSet<DatoUsuario> DatoUsuarios { get; set; }
        public DbSet<PasoDependencia> PasoDependencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales si las necesitas, por ejemplo:
            modelBuilder.Entity<Flujo>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Paso>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Campo>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<DatoUsuario>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<PasoDependencia>()
                .HasKey(pd => pd.Id);

            // Relación de uno a muchos entre Flujo y Paso
            modelBuilder.Entity<Flujo>()
                .HasMany(f => f.Pasos)
                .WithOne(p => p.Flujo)
                .HasForeignKey(p => p.FlujoId);

            // Relación de uno a muchos entre Paso y Campo
            modelBuilder.Entity<Paso>()
                .HasMany(p => p.Campos)
                .WithOne(c => c.Paso)
                .HasForeignKey(c => c.PasoId);

            // Relación entre Campo y CampoCatalogo
     
          
            modelBuilder.Entity<Campo>()
               .HasOne(c => c.CampoCatalogo)
               .WithMany(c => c.UsosEnPasos)
               .HasForeignKey(c => c.CampoCatalogoId)
               .OnDelete(DeleteBehavior.Restrict);

            // Relación de dependencia de pasos
            modelBuilder.Entity<PasoDependencia>()
                .HasOne(pd => pd.Paso)
                .WithMany(p => p.Dependencias)
                .HasForeignKey(pd => pd.PasoId);
        }
    }
}
