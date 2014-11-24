using System.Collections.Generic;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Models
{
    public class Categoria
    {
        public List<SubCategoria> ListOfSubCategorias { get; set; }
        public string NombreCategoria { get; set; }
        public string LogoUrl { get; set; }

        public Categoria()
        {
            this.ListOfSubCategorias = new List<SubCategoria>();
        }
    }
}