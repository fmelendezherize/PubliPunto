using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string DetallesBig { get; set; }
        public string Condiciones { get; set; }
        public int CuponesPorUsuario { get; set; }

        public string ImagenUrl { get; set; }
        public string ImagenSmallUrl { get; set; }

        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFin { get; set; }

        public bool IsActivo { get; set; }

        public virtual IList<PromocionCupon> ListOfPromocionCupons { get; set; }

        [NotMapped]
        public bool HasCuponesDisponibles
        {
            get
            {
                return CheckHasCuponesDisponibles();
            }
        }

        [NotMapped]
        public bool IsVigente
        {
            get
            {
                return CheckIsPromocionVigente();
            }
        }

        public Promocion()
        {
            this.FechaFin = System.DateTime.Now;
            this.FechaInicio = System.DateTime.Now;
            this.CuponesPorUsuario = 1;
        }

        private bool CheckIsPromocionVigente()
        {
            if (IsActivo &&
                EnteComercial.IsActivo &&
                HasCuponesDisponibles &&
                (FechaInicio <= System.DateTime.Now) && (FechaFin >= System.DateTime.Now)) return true;
            return false;
        }

        private bool CheckHasCuponesDisponibles()
        {
            return (this.ListOfPromocionCupons.Any(q => q.UsuarioAsignadoId == null));
        }

        public PromocionCupon ObtenerCupon(Usuario usuarioSelected)
        {
            var cuponesAsignadosToUsuario = (from q in this.ListOfPromocionCupons
                                             where (q.UsuarioAsignadoId == usuarioSelected.Id)
                                             select q).ToList();

            if (cuponesAsignadosToUsuario.Count == this.CuponesPorUsuario)
            {
                if (cuponesAsignadosToUsuario.Count == 1)
                {
                    //return cuponesAsignadosToUsuario.FirstOrDefault();
                    throw new System.InvalidOperationException("Máximo un cupón (1) por persona para esta promoción.");
                }
                if (cuponesAsignadosToUsuario.Count != 0)
                {
                    throw new System.InvalidOperationException("Maximo número de cupones alcanzados para esta promoción.");
                }
            }

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
