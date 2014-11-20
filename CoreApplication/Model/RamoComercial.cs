using System.ComponentModel.DataAnnotations;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class RamoComercial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Nombre { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public string ImagenUrl { get; set; }
    }
}
