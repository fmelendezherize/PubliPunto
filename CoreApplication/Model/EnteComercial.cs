using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class EnteComercial
    {
        [Key]
        public int Id { get; set; }

        [MinLength(1), MaxLength(200)]
        public string Nombre { get; set; }

        public RamoComercial RamoComercial { get; set; }
    }
}
