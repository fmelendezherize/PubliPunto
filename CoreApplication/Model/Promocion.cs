using System.ComponentModel.DataAnnotations;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class Promocion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Codigo { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Descripcion { get; set; }
    }
}
