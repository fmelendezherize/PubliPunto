using System.ComponentModel.DataAnnotations;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class DatosContacto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Comentario { get; set; }

        [MinLength(1)]
        public string Destinatario { get; set; }
    }
}
