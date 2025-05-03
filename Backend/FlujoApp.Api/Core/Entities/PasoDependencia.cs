namespace FlujoApp.Api.Core.Entities
{
    public class PasoDependencia
    {
        public Guid Id { get; set; }

        public Guid PasoId { get; set; }              // Paso actual
        public Paso Paso { get; set; }

        public Guid DependeDePasoId { get; set; }     // Paso del que depende
    }
}
