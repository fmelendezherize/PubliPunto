using Decktra.PubliPuntoEstacion.CoreApplication.Context;
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Repository
{
    public class EnteComercialRepository : IDisposable
    {
        private PubliPuntoContext db { get; set; }

        public EnteComercialRepository()
        {
            db = new PubliPuntoContext();
        }

        public EnteComercial FindBy(int id)
        {
            return db.EnteComercials.Include("ListOfPromocions.ListOfPromocionCupons").
                Where(q => q.Id == id).
                FirstOrDefault();
        }

        public IEnumerable<EnteComercial> GetEnteComercialsBy(int idRamoComercial)
        {
            return (from q in GetEnteComercialesWithPromocionActiva()
                    where (q.RamoComercial.Id == idRamoComercial)
                    select q);
        }

        public IEnumerable<EnteComercial> GetEnteComercialsByName(string nombre)
        {
            var ret = (from q in GetEnteComercialesWithPromocionActiva()
                       where (q.Nombre.StartsWith(nombre, System.StringComparison.InvariantCultureIgnoreCase))
                       select q);
            return ret.ToList<EnteComercial>();
        }

        public IEnumerable<EnteComercial> GetEnteComercialesActivos()
        {
            return db.EnteComercials.Where(q => q.IsActivo).ToList();
        }

        public IEnumerable<EnteComercial> GetEnteComercialsByTags(string Tag)
        {
            var listOfResult = this.GetEnteComercialesWithPromocionActiva().Where(q => q.TagMatched(Tag));
            return listOfResult.ToList();
        }

        public IEnumerable<EnteComercial> GetEnteComercialesWithPromocionActiva()
        {
            var result = from q in this.GetPromocionesVigentes().GroupBy(grp => grp.EnteComercialId)
                         select q.FirstOrDefault();

            return (from q in result select q.EnteComercial).ToList();
        }

        public IEnumerable<Promocion> GetPromocionesVigentes()
        {
            //var result = (from q in db.Promociones
            //              where ((q.IsActivo) && (q.EnteComercial.IsActivo) &&
            //              (q.FechaInicio <= System.DateTime.Now) && (q.FechaFin >= System.DateTime.Now))
            //              orderby q.FechaInicio descending, q.Id descending
            //              select q).ToList();

            var result = db.Promociones.
                OrderByDescending(q => q.FechaInicio).
                OrderByDescending(q => q.Id).
                ToList().
                Where(q => q.IsVigente);

            return result;
        }

        public IEnumerable<Promocion> GetPromocionesActivasBy(int idEnteComercial)
        {
            return this.GetPromocionesVigentes().Where(q => q.EnteComercialId == idEnteComercial).ToList();
        }

        public IEnumerable<string> GetAutoCompleteTags()
        {
            List<string> listOfTags = new List<string>();

            var result = from q in this.GetEnteComercialesWithPromocionActiva()
                         select q.Nombre.ToLowerInvariant();
            listOfTags.AddRange(result.ToList());

            this.GetEnteComercialesWithPromocionActiva().ToList().ForEach(q =>
            {
                listOfTags.AddRange((from j in q.GetListOfTags() select j.ToLowerInvariant()).ToList());
            });

            return listOfTags.Distinct().ToList();
        }

        public void AddOrUpdate(EnteComercialDTO dto)
        {
            var ramoComercial = db.RamoComercials.Where((q) => q.Codigo == dto.Categoria).FirstOrDefault();
            if (ramoComercial == null) return;

            var enteComercial = (from q in db.EnteComercials where (q.Codigo == dto.Codigo) select q).FirstOrDefault();
            if (enteComercial == null)
            {
                EnteComercial newEnteComercial = new EnteComercial()
                {
                    Codigo = dto.Codigo,
                    Direccion = dto.Direccion,
                    IsActivo = dto.IsActivo,
                    Nombre = dto.Nombre,
                    Rif = dto.Rif,
                    Tags = dto.Tags,
                    Telefonos = dto.Telefonos,
                    WebAddress = dto.WebAddress,
                };
                newEnteComercial.ImagenUrl = dto.ImagenURL.image.ToFileName();
                newEnteComercial.LogoUrl = dto.LogoURL.logo.ToFileName();
                newEnteComercial.RamoComercial = ramoComercial;
                db.EnteComercials.Add(newEnteComercial);
            }
            else
            {
                enteComercial.Codigo = dto.Codigo;
                enteComercial.Direccion = dto.Direccion;
                enteComercial.IsActivo = dto.IsActivo;
                enteComercial.Nombre = dto.Nombre;
                enteComercial.Rif = dto.Rif;
                enteComercial.Tags = dto.Tags;
                enteComercial.Telefonos = dto.Telefonos;
                enteComercial.WebAddress = dto.WebAddress;
                enteComercial.ImagenUrl = dto.ImagenURL.image.ToFileName();
                enteComercial.LogoUrl = dto.LogoURL.logo.ToFileName();
                enteComercial.RamoComercial = ramoComercial;
            }
            db.Entry(ramoComercial).State = System.Data.Entity.EntityState.Unchanged;
            db.SaveChanges();
        }

        public void AddOrUpdatePromocion(Kiosko_Promocion_Detalle dto)
        {
            if (dto == null) return;

            var enteComercial = (from q in db.EnteComercials.Include("ListOfPromocions.ListOfPromocionCupons")
                                 where (q.Codigo == dto.ClienteCodigo)
                                 select q).FirstOrDefault();
            if (enteComercial == null) return;

            var promocion = (from q in enteComercial.ListOfPromocions where (q.Codigo == dto.Codigo) select q).FirstOrDefault();
            if (promocion == null)
            {
                var newPromocion = new Promocion()
                {
                    Codigo = dto.Codigo,
                    Descripcion = dto.Descripcion,
                    Detalles = dto.Detalles,
                    DetallesBig = dto.DetallesBig,
                    Condiciones = dto.Condiciones,
                    IsActivo = true
                };
                newPromocion.FechaInicio = DateTime.Parse(dto.Inicio);
                newPromocion.FechaFin = DateTime.Parse(dto.Fin);
                newPromocion.ImagenSmallUrl = dto.ImagenSmallUrl.card.ToFileName();
                newPromocion.ImagenUrl = dto.ImagenUrl.banner.ToFileName();
                if (dto.Limite != null)
                {
                    newPromocion.CuponesPorUsuario = int.Parse(dto.Limite);
                }

                newPromocion.ListOfPromocionCupons = new List<PromocionCupon>();
                foreach (var item in dto.Kiosko_Promociones)
                {
                    var newCupon = new PromocionCupon()
                    {
                        CodigoCanjeo = item.Codigo,
                        RemoteId = item.ID,
                    };
                    newPromocion.ListOfPromocionCupons.Add(newCupon);
                }
                enteComercial.ListOfPromocions.Add(newPromocion);
            }
            else
            {
                promocion.Descripcion = dto.Descripcion;
                promocion.Detalles = dto.Detalles;
                promocion.DetallesBig = dto.DetallesBig;
                promocion.Condiciones = dto.Condiciones;
                promocion.FechaInicio = DateTime.Parse(dto.Inicio);
                promocion.FechaFin = DateTime.Parse(dto.Fin);
                promocion.ImagenSmallUrl = dto.ImagenSmallUrl.card.ToFileName();
                promocion.ImagenUrl = dto.ImagenUrl.banner.ToFileName();
                if (dto.Limite != null)
                {
                    promocion.CuponesPorUsuario = int.Parse(dto.Limite);
                }

                if (promocion.ListOfPromocionCupons != null)
                {
                    foreach (var item in dto.Kiosko_Promociones)
                    {
                        var cupon = (from q in promocion.ListOfPromocionCupons where (q.RemoteId == item.ID) select q).FirstOrDefault();
                        if (cupon == null)
                        {
                            var newCupon = new PromocionCupon()
                            {
                                CodigoCanjeo = item.Codigo,
                                RemoteId = item.ID,
                            };
                            promocion.ListOfPromocionCupons.Add(newCupon);
                        }
                    }
                }
            }
            db.SaveChanges();
        }

        public PromocionCupon UpdatePromocionCupon(Promocion promocionSelected, Usuario usuarioSelected)
        {
            var promocion = db.Promociones.Where(q => q.Id == promocionSelected.Id).FirstOrDefault();
            if (promocion == null) return null;

            var cupon = promocion.ObtenerCupon(usuarioSelected);
            db.SaveChanges();

            return cupon;
        }

        public void UpdatePromocionCuponBySmsSent(int promocionCuponId)
        {
            var promocionCupon = db.PromocionCupones.Find(promocionCuponId);
            if (promocionCupon == null) return;

            promocionCupon.SmsSent = true;
            db.SaveChanges();
        }

        public void Dispose()
        {
            if (db == null) return;
            db.Dispose();
        }
    }
}
