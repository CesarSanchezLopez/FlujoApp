namespace FlujoApp.Api.Dtos
{
    public class PasoDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int Orden { get; set; }

        public List<string> Dependencias { get; set; } = new();
        public List<CampoDto> Campos { get; set; } = new();
    }
}
