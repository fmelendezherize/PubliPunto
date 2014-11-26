
using System;
using System.Collections.Generic;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos
{
    public class EnteComercialDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Rif { get; set; }
        public string Telefonos { get; set; }
        public string Direccion { get; set; }
        public string WebAddress { get; set; }
        public string Tags { get; set; }
        public bool IsActivo { get; set; }

        public ImagenUrlDTO ImagenURL { get; set; }
        public LogoUrlDTO LogoURL { get; set; }
        public string VideoUrl { get; set; }

        //Codigo Ramo Comercial
        public string Categoria { get; set; }

        public string ToImagenFileName()
        {
            if (ImagenURL.image.url == null) return null;
            Uri webpath = new Uri("file://localhost" + ImagenURL.image.url);
            if (webpath.IsFile)
            {
                return System.IO.Path.GetFileName(webpath.LocalPath);
            }
            return null;
        }

        public string ToLogoFileName()
        {
            if (LogoURL.logo.url == null) return null;
            Uri webpath = new Uri("file://localhost" + LogoURL.logo.url);
            if (webpath.IsFile)
            {
                return System.IO.Path.GetFileName(webpath.LocalPath);
            }
            return null;
        }
    }

    public class ListOfEnteComercialDTO
    {
        public List<EnteComercialDTO> Entes_Comerciales { get; set; }
    }
}
