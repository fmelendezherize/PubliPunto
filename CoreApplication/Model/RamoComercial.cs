using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class RamoComercial
    {
        [Key]
        public int Id { get; set; }
        
        [MinLength(1), MaxLength(200)]
        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }
    }
}
