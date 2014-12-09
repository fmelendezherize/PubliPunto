
using System.ComponentModel.DataAnnotations;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [MinLength(0), MaxLength(10)]
        public string Cedula { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
    }
}
