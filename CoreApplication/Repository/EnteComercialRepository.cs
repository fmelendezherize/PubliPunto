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

        }

        public void Dispose()
        {
            if (db == null) return;
            db.Dispose();
        }
    }
}
