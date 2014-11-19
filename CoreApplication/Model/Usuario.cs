
using System.Collections.Generic;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class Usuario
    {
        public string Cedula { get; set; }
        public string Codigo { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
    }

    public class ListOfUsuario
    {
        public List<Usuario> Usuarios { get; set; }
    }
}
