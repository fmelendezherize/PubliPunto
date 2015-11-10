
using System.Collections.Generic;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos
{
    public class Kiosko_PromocionDTO
    {
        //public string ClienteCodigo { get; set; }

        public string Codigo { get; set; }
        //public string Descripcion { get; set; }
        //public string Detalles { get; set; }
        //public string DetallesBig { get; set; }
        //public string Condiciones { get; set; }
        //public string ImagenUrl { get; set; }
        //public string ImagenSmallUrl { get; set; }

        //public string Fin { get; set; }
        //public string Inicio { get; set; }
    }

    public class ListOfKiosko_PromocionesDTO
    {
        public List<Kiosko_PromocionDTO> Kiosko_Promociones { get; set; }
    }

    public class Kiosko_Promocion_Detalle
    {
        public string ClienteCodigo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Detalles { get; set; }
        public string DetallesBig { get; set; }
        public string Condiciones { get; set; }
        public string ImagenUrl { get; set; }
        public string ImagenSmallUrl { get; set; }

        public string Fin { get; set; }
        public string Inicio { get; set; }

        public List<Kiosko_Promocion_Cupon> Kiosko_Promociones { get; set; }
    }

    public class Kiosko_Promocion_Cupon
    {
        public string Codigo { get; set; }
        public string ID { get; set; }
    }
}
