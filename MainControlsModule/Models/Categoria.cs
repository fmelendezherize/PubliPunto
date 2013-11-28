using System.Collections.Generic;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Models
{
    public class Categoria
    {
        public List<SubCategoria> ListCategorias { get; set; }
        public string NombreCategoria { get; set; }

        public Categoria()
        {
            this.ListCategorias = new List<SubCategoria>();
        }
    }
}