
using Decktra.PubliPuntoEstacion.CoreApplication.Context;
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System;
namespace Decktra.PubliPuntoEstacion.CoreApplication.Repository
{
    public class DatosContactosRepository : IDisposable
    {
        private PubliPuntoContext db { get; set; }

        public DatosContactosRepository()
        {
            db = new PubliPuntoContext();
        }

        public void Add(DatosContacto newDatosContacto)
        {
            db.DatosContatos.Add(newDatosContacto);
            db.SaveChanges();
        }

        public void Dispose()
        {
            if (db == null) return;
            db.Dispose();
        }
    }
}
