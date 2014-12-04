using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class Promocion
    {
        [Key]
        public int Id { get; set; }

        public int EnteComercialId { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Codigo { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Descripcion { get; set; }

        public string Detalles { get; set; }
        public System.DateTime FechaFin { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public bool IsActivo { get; set; }

        public virtual IList<PromocionCupon> ListOfPromocionCupons { get; set; }
    }

    public class PromocionCupon
    {
        [Key]
        public int Id { get; set; }

        public int PromocionId { get; set; }

        public string RemoteId { get; set; }
        public string CodigoCanjeo { get; set; }

        public int? UsuarioAsignadoId { get; set; }
        public virtual Usuario UsuarioAsignado { get; set; }
    }
}
