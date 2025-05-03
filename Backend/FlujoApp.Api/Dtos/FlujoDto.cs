namespace FlujoApp.Api.Dtos
{
    public class FlujoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<PasoDto> Pasos { get; set; } = new();
    }
}
