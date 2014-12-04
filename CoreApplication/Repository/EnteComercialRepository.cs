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
            return (from q in db.EnteComercials where (q.Id == id) select q).FirstOrDefault();
        }

        public IEnumerable<EnteComercial> GetEnteComercialsBy(int idRamoComercial)
        {
            return (from q in db.EnteComercials where (q.RamoComercial.Id == idRamoComercial) select q);
        }

        public IEnumerable<EnteComercial> GetEnteComercialsByName(string nombre)
        {
            var ret = (from q in db.EnteComercials
                       where (q.Nombre.StartsWith(nombre))
                       select q);
            return ret.ToList<EnteComercial>();
        }

        public IEnumerable<EnteComercial> GetAll()
        {
            return db.EnteComercials.ToList();
        }

        public IEnumerable<EnteComercial> GetEnteComercialsByTags(string Tag)
        {
            var listOfResult = this.GetAll().Where(q => q.TagMatched(Tag));
            return listOfResult.ToList();
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
                newEnteComercial.ImagenUrl = dto.ToImagenFileName();
                newEnteComercial.LogoUrl = dto.ToLogoFileName();
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
                enteComercial.ImagenUrl = dto.ToImagenFileName();
                enteComercial.LogoUrl = dto.ToLogoFileName();
                enteComercial.RamoComercial = ramoComercial;
            }
            db.Entry(ramoComercial).State = System.Data.Entity.EntityState.Unchanged;
            db.SaveChanges();
        }

        public void AddOrUpdatePromocion(Kiosko_Promocion_Detalle dto)
        {
            var enteComercial = (from q in db.EnteComercials where (q.Codigo == dto.ClienteCodigo) select q).FirstOrDefault();
            if (enteComercial == null) return;

            var promocion = (from q in enteComercial.ListOfPromocions where (q.Codigo == dto.Codigo) select q).FirstOrDefault();
            if (promocion == null)
            {
                var newPromocion = new Promocion()
                {
                    Codigo = dto.Codigo,
                    Descripcion = dto.Descripcion,
                    Detalles = dto.Detalles,
                    IsActivo = true
                };
                newPromocion.FechaInicio = DateTime.Parse(dto.Inicio);
                newPromocion.FechaFin = DateTime.Parse(dto.Fin);
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
                promocion.FechaInicio = DateTime.Parse(dto.Inicio);
                promocion.FechaFin = DateTime.Parse(dto.Fin);

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

        public void Dispose()
        {
            if (db == null) return;
            db.Dispose();
        }
    }
}
