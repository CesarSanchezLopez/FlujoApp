namespace FlujoApp.Api.Core.Entities
{
    public class Campo
    {
        public Guid Id { get; set; }
        public Guid PasoId { get; set; }
        public Paso Paso { get; set; } = null!;

        // Campo tomado del catálogo maestro
        public string CampoCodigo { get; set; } = null!;
        public Guid CampoCatalogoId { get; set; }
        public CampoCatalogo CampoCatalogo { get; set; } = null!;

        public bool EsEntrada { get; set; }

   
    }
}
