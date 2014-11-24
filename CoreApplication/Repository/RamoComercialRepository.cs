using Decktra.PubliPuntoEstacion.CoreApplication.Context;
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Repository
{
    public class RamoComercialRepository : IDisposable
    {
        private PubliPuntoContext db { get; set; }

        public RamoComercialRepository()
        {
            db = new PubliPuntoContext();
        }

        public IEnumerable<RamoComercial> GetAll()
        {
            return db.RamoComercials.ToList<RamoComercial>();
        }

        public IEnumerable<RamoComercial> GetAllByFirstLetter(string letter)
        {
            var ret = from q in db.RamoComercials where (q.Nombre.StartsWith(letter)) select q;
            return ret.ToList<RamoComercial>();
        }

        public RamoComercial FindBy(int id)
        {
            return (from q in db.RamoComercials where (q.Id == id) select q).FirstOrDefault();
        }

        public void AddOrUpdate(RamoComercialDTO dto)
        {
            var ramoComercial = (from q in db.RamoComercials where (q.Codigo == dto.Codigo) select q).FirstOrDefault();
            if (ramoComercial == null)
            {
                RamoComercial newRamoComercial = new RamoComercial()
                {
                    Codigo = dto.Codigo,
                    Nombre = dto.Descripcion,
                };
                newRamoComercial.LogoUrl = dto.ToImagenFileName();
                db.RamoComercials.Add(newRamoComercial);
            }
            else
            {
                ramoComercial.Nombre = dto.Descripcion;
                ramoComercial.LogoUrl = dto.ToImagenFileName();
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
