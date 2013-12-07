namespace Decktra.PubliPuntoEstacion.MainControlsModule.Models
{
    public enum TipoSubCategoria 
    { 
        RamoComercial,
        EnteComercial
    }

    public class SubCategoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoSubCategoria TipoSubCategoria { get; set; }

        public SubCategoria()
        {
        }
    }
}