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
        public DbSet<PasoDependencia> PasoDependencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flujo>().ToTable("Flujos");
            modelBuilder.Entity<Paso>().ToTable("Pasos");
            modelBuilder.Entity<Campo>().ToTable("Campos");
            modelBuilder.Entity<PasoDependencia>().ToTable("PasoDependencias");

            modelBuilder.Entity<PasoDependencia>()
                .HasOne<Paso>()
                .WithMany()
                .HasForeignKey(pd => pd.DependeDePasoId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
