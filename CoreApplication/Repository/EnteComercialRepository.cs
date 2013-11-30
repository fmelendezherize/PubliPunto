using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decktra.PubliPuntoEstacion.CoreApplication.Model;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Repository
{
    public class EnteComercialRepository
    {
        private List<EnteComercial> _enteComercials;

        public EnteComercialRepository()
        {
            Seed();
        }

        private void Seed()
        {
            _enteComercials = new List<EnteComercial>();
            var ramoComercial = new RamoComercial{ Id=3, Nombre = "Bancos"};
            
            _enteComercials.Add(new EnteComercial {Id = 0, Nombre = "Banco Mercantil", RamoComercial = ramoComercial});
            _enteComercials.Add(new EnteComercial { Id = 1, Nombre = "Banesco", RamoComercial = ramoComercial });
            _enteComercials.Add(new EnteComercial { Id = 2, Nombre = "Banco Provincial", RamoComercial = ramoComercial });

            ramoComercial = new RamoComercial {Id = 4, Nombre = "Comida Rapida"};
            _enteComercials.Add(new EnteComercial { Id = 3, Nombre = "Pizza Hut", RamoComercial = ramoComercial });
            _enteComercials.Add(new EnteComercial { Id = 4, Nombre = "Mac Donald", RamoComercial = ramoComercial });
        }

        public EnteComercial FindBy(int id)
        {
            return (from q in _enteComercials where (q.Id == id) select q).FirstOrDefault();
        }

        public IEnumerable<EnteComercial> GetEnteComercialsBy(int idRamoComercial)
        {
            return (from q in _enteComercials where (q.RamoComercial.Id == idRamoComercial) select q);
        }
    }
}
