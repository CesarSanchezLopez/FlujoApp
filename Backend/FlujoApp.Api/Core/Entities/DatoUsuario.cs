namespace FlujoApp.Api.Core.Entities
{
    public class DatoUsuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid FlujoId { get; set; }
        public Guid? PasoId { get; set; } // Opcional, útil para trazabilidad

        public string CampoCodigo { get; set; } = null!; // F-0001, etc.
        public string Valor { get; set; } = null!;
    }
}
