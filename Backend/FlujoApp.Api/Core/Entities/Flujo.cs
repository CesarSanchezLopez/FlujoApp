namespace FlujoApp.Api.Core.Entities
{
    public class Flujo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Paso> Pasos { get; set; } = new List<Paso>();
    }
}
