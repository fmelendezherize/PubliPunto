
using System.ComponentModel.DataAnnotations;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [MinLength(0), MaxLength(10)]
        public string Cedula { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Codigo { get; set; }

        public string Email { get; set; }
        public string Pin { get; set; }
    }
}
