using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class EnteComercial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Nombre { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Codigo { get; set; }

        public string Rif { get; set; }
        public string Telefonos { get; set; }
        public string Direccion { get; set; }
        public string WebAddress { get; set; }
        public string LogoUrl { get; set; }
        public string ImagenUrl { get; set; }
        public string VideoUrl { get; set; }

        [Required]
        public virtual RamoComercial RamoComercial { get; set; }

        public string Tags { get; set; }

        public bool IsActivo { get; set; }

        public virtual IList<Promocion> ListOfPromocions { get; set; }

        public EnteComercial()
        {
            this.ImagenUrl = string.Empty;
            this.VideoUrl = string.Empty;
        }

        public bool TagMatched(string Tag)
        {
            var listOfTags = Tags.Split(';');
            foreach (string s in listOfTags)
            {
                if (s.StartsWith(Tag, System.StringComparison.InvariantCultureIgnoreCase)) return true;
            }
            return false;
        }
    }
}
