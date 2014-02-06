using Decktra.PubliPuntoEstacion.CoreApplication.Context;
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System.Collections.Generic;
using System.Linq;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Repository
{
    public class RamoComercialRepository
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
    }
}
