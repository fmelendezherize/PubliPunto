using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using System.Data.Entity;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Context
{
    class PubliPuntoContext : DbContext
    {
        public DbSet<RamoComercial> RamoComercials { get; set; }
        public DbSet<EnteComercial> EnteComercials { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
