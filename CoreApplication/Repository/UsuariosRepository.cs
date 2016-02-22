
using Decktra.PubliPuntoEstacion.CoreApplication.Context;
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Model.Dtos;
using System;
using System.Linq;

namespace Decktra.PubliPuntoEstacion.CoreApplication.Repository
{
    public class UsuariosRepository : IDisposable
    {
        private PubliPuntoContext db { get; set; }

        public UsuariosRepository()
        {
            db = new PubliPuntoContext();
        }

        public void AddOrUpdate(UsuarioDTO dto)
        {
            var usuario = (from q in db.Usuarios where (q.Codigo == dto.Codigo) select q).FirstOrDefault();
            if (usuario == null)
            {
                Usuario newUsuario = new Usuario
                {
                    Cedula = dto.Cedula,
                    Codigo = dto.Codigo,
                    Email = dto.Email,
                    Pin = dto.Pin,
                    Nombre = dto.Nombre
                };
                db.Usuarios.Add(newUsuario);
            }
            else
            {
                usuario.Cedula = dto.Cedula;
                usuario.Email = dto.Email;
                usuario.Pin = dto.Pin;
                usuario.Nombre = dto.Nombre;
            }
            db.SaveChanges();
        }

        public Usuario RetrieveUsuarioBy(string cedula, string pin)
        {
            var usuario = (from q in db.Usuarios where ((q.Cedula == cedula) & (q.Pin == pin)) select q).FirstOrDefault();
            return usuario;
        }

        public void AddOrUpdate(Usuario newUsuario)
        {
            if (!newUsuario.IsValido()) { throw new InvalidOperationException("Datos Usuarios Invalidos."); }

            var usuario = db.Usuarios.Where(q => q.Cedula == newUsuario.Cedula).FirstOrDefault();
            if (usuario == null)
            {
                db.Usuarios.Add(newUsuario);
            }
            else
            {
                usuario.Nombre = newUsuario.Nombre;
                usuario.Movil = newUsuario.Movil;
                newUsuario.Id = usuario.Id;
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
