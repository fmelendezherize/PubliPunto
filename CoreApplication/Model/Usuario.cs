
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class Usuario
    {
        const string patronMovil = "(0412|0424|0416|0414|0426)";

        [Key]
        public int Id { get; set; }

        [MinLength(0), MaxLength(10)]
        public string Cedula { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Movil { get; set; }
        public string Pin { get; set; }


        public bool IsValido()
        {
            if (this.Cedula.Length < 7) return false;
            if (this.Nombre.Length < 7) return false;

            if (!String.IsNullOrEmpty(this.Movil))
            {
                if (this.Movil.Length != 11) return false;

                string codigoMovil = this.Movil.Substring(0, 4);
                if (!Regex.IsMatch(codigoMovil, patronMovil)) return false;
            }
            return true;
        }
    }
}
