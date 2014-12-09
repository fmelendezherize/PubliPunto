
using System.Collections.Generic;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos
{
    public class UsuarioDTO
    {
        public string Cedula { get; set; }
        public string Codigo { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
        public string Nombre { get; set; }
    }

    public class ListOfUsuario
    {
        public List<UsuarioDTO> Usuarios { get; set; }
    }
}
