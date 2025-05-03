namespace FlujoApp.Api.Core.Entities
{
    public class Paso
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }  // Ej: STP-0001
        public string Nombre { get; set; }
        public string Tipo { get; set; }    // Ej: RegistroUsuario, EnviarCorreo
        public int Orden { get; set; }

        public Guid FlujoId { get; set; }
        public Flujo Flujo { get; set; }

        public ICollection<PasoDependencia> Dependencias { get; set; } = new List<PasoDependencia>();
        public ICollection<Campo> Campos { get; set; } = new List<Campo>();
    }
}
