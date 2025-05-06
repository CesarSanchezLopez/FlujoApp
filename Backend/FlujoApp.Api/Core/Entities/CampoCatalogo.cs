namespace FlujoApp.Api.Core.Entities
{
    public class CampoCatalogo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Codigo { get; set; } = null!; // Ej: F-0001
        public string Nombre { get; set; } = null!; // Ej: Primer nombre
        public string Tipo { get; set; } = null!;   // Texto, Número, Fecha, etc.
        public bool Requerido { get; set; } = true;

        // Relación inversa opcional
        public ICollection<Campo>? UsosEnPasos { get; set; }
    }
}
