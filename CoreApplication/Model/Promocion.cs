using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Model
{
    public class Promocion
    {
        [Key]
        public int Id { get; set; }

        public int EnteComercialId { get; set; }
        public virtual EnteComercial EnteComercial { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Codigo { get; set; }

        [Required]
        [MinLength(1), MaxLength(200)]
        public string Descripcion { get; set; }
        public string Detalles { get; set; }
        public string Condiciones { get; set; }

        public System.DateTime FechaFin { get; set; }
        public System.DateTime FechaInicio { get; set; }

        public bool IsActivo { get; set; }

        public virtual IList<PromocionCupon> ListOfPromocionCupons { get; set; }

        public Promocion()
        {
            this.FechaFin = System.DateTime.Now.AddDays(7);
            this.FechaInicio = System.DateTime.Now;
        }

        public PromocionCupon ObtenerCupon(Usuario usuarioSelected)
        {
            var promocionCupon = (from q in this.ListOfPromocionCupons
                                  where (q.UsuarioAsignadoId == usuarioSelected.Id)
                                  select q).FirstOrDefault();
            if (promocionCupon != null) return promocionCupon;

            //nuevo Cupon
            var newPromocionCupon = (from q in this.ListOfPromocionCupons
                                     where (q.UsuarioAsignadoId == null)
                                     select q).FirstOrDefault();
            if (newPromocionCupon != null)
            {
                newPromocionCupon.UsuarioAsignadoId = usuarioSelected.Id;
            }

            return newPromocionCupon;
        }
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

        public bool SmsSent { get; set; }
    }
}
