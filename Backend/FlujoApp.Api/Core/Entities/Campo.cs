namespace FlujoApp.Api.Core.Entities
{
    public class Campo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; } // Texto, Número, Fecha, etc.
        public bool Requerido { get; set; }

        public Guid PasoId { get; set; }
        public Paso Paso { get; set; }
    }
}
