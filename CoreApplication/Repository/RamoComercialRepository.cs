using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System.Collections.Generic;
using System.Linq;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Repository
{
    public class RamoComercialRepository
    {
        private List<RamoComercial> _ramoComercials;

        public RamoComercialRepository()
        {
            Seed();
        }

        private void Seed()
        {
            _ramoComercials = new List<RamoComercial>();
            var ramoComercial = new RamoComercial
            {
                Id = 0,
                Nombre = "Animales",
            };
            _ramoComercials.Add(ramoComercial);

            _ramoComercials.Add(new RamoComercial { Id = 1, Nombre = "Alimentos" });
            _ramoComercials.Add(new RamoComercial { Id = 2, Nombre = "Antiguedades" });
            _ramoComercials.Add(new RamoComercial { Id = 3, Nombre = "Bancos" });
            _ramoComercials.Add(new RamoComercial { Id = 4, Nombre = "Comida Rapida" });
            _ramoComercials.Add(new RamoComercial { Id = 5, Nombre = "Muebles" });
            _ramoComercials.Add(new RamoComercial { Id = 6, Nombre = "Peluquerias" });
            _ramoComercials.Add(new RamoComercial { Id = 7, Nombre = "Seguridad" });
        }

        public IEnumerable<RamoComercial> GetAll()
        {
            return _ramoComercials;
        }

        public IEnumerable<RamoComercial> GetAllByFirstLetter(string letter)
        {
            var ret = from q in _ramoComercials where q.Nombre.StartsWith(letter) select q;
            return ret;
        }

        public RamoComercial FindBy(int id)
        {
            return (from q in _ramoComercials where (q.Id == id) select q).FirstOrDefault();
        }
    }
}
