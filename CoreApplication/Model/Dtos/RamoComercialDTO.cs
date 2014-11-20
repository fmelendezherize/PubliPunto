﻿
using System.Collections.Generic;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos
{
    public class RamoComercialDTO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Kiosko { get; set; }
        public ImagenUrlDTO ImagenURL { get; set; }
    }

    public class ImagenUrlDTO
    {
        public ImageDTO image { get; set; }
    }

    public class ImageDTO
    {
        public string url { get; set; }
    }

    public class ListOfRamoComercialDTO
    {
        public List<RamoComercialDTO> Ramos_Comerciales { get; set; }
    }
}