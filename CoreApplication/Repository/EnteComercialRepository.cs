using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System.Collections.Generic;
using System.Linq;

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
            var ramoComercial = new RamoComercial { Id = 3, Nombre = "Bancos" };

            _enteComercials.Add(new EnteComercial { Id = 0, Nombre = "Banco Mercantil", RamoComercial = ramoComercial });
            _enteComercials.Add(new EnteComercial { Id = 1, Nombre = "Banesco", RamoComercial = ramoComercial });
            _enteComercials.Add(new EnteComercial { Id = 2, Nombre = "Banco Provincial", RamoComercial = ramoComercial });

            ramoComercial = new RamoComercial { Id = 4, Nombre = "Comida Rapida" };
            _enteComercials.Add(new EnteComercial
            {
                Id = 3,
                Nombre = "Pizza Hut",
                RamoComercial = ramoComercial,
                Rif = "J-123-456-789",
                Telefonos = "0251.251.11.11 - 0416.111.11.11",
                Direccion = "Av. Lara con Av. Leones, Local 05, al lado del Burguer King",
                WebAddress = "http://www.pizzahut.net.ve/",
                Promocion = "Pizza 2 x 1 los miercoles y jueves"
            });
            _enteComercials.Add(new EnteComercial
            {
                Id = 4,
                Nombre = "Mac Donald",
                RamoComercial = ramoComercial
            });
        }

        public EnteComercial FindBy(int id)
        {
            return (from q in _enteComercials where (q.Id == id) select q).FirstOrDefault();
        }

        public IEnumerable<EnteComercial> GetEnteComercialsBy(int idRamoComercial)
        {
            return (from q in _enteComercials where (q.RamoComercial.Id == idRamoComercial) select q);
        }

        public IEnumerable<EnteComercial> GetEnteComercialsByName(string nombre)
        {
            return (from q in _enteComercials where (q.Nombre.Contains(nombre)) select q);
        }
    }
}
