
using System;
using System.Collections.Generic;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos
{
    public class RamoComercialDTO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Kiosko { get; set; }
        public ImagenUrlDTO ImagenURL { get; set; }

        public string ToImagenFileName()
        {
            Uri webpath = new Uri("file://localhost" + ImagenURL.image.url);
            if (webpath.IsFile)
            {
                return System.IO.Path.GetFileName(webpath.LocalPath);
            }
            return null;
        }
    }

    public class ListOfRamoComercialDTO
    {
        public List<RamoComercialDTO> Ramos_Comerciales { get; set; }
    }
}
