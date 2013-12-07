using System.ComponentModel.DataAnnotations;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class EnteComercial
    {
        [Key]
        public int Id { get; set; }

        [MinLength(1), MaxLength(200)]
        public string Nombre { get; set; }
        public string Rif { get; set; }
        public string Telefonos { get; set; }
        public string Direccion { get; set; }
        public string WebAddress { get; set; }
        public string Promocion { get; set; }

        public RamoComercial RamoComercial { get; set; }
    }
}
