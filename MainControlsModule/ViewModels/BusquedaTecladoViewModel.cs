﻿
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    class BusquedaTecladoViewModel
    {
        public ObservableCollection<Categoria> Categorias { get; set; }
        private readonly EnteComercialRepository _enteComercialRepository;
        public ICommand SearchEnteComercialsCommand { get; set; }

        public BusquedaTecladoViewModel()
        {
            this._enteComercialRepository = new EnteComercialRepository();
            this.SearchEnteComercialsCommand = new DelegateCommand<string>(this.SearchEnteComercials);
            Categorias = new ObservableCollection<Categoria>();
        }

        public void Init()
        {
            Categorias.Clear();
        }

        private void SearchEnteComercials(string obj)
        {
            if (string.IsNullOrEmpty(obj)) return;

            var newCategoria = new Categoria
            {
                NombreCategoria = "Resultados de la búsqueda",
                ListCategorias = (from q in _enteComercialRepository.GetEnteComercialsByName(obj)
                                  select new SubCategoria()
                                  {
                                      Id = q.Id,
                                      Nombre = q.Nombre,
                                      LogoUrl = q.LogoUrl,
                                      TipoSubCategoria = TipoSubCategoria.EnteComercial
                                  }).ToList()
            };
            Categorias.Clear();
            if (newCategoria.ListCategorias.Count == 0) newCategoria.NombreCategoria = "No se consiguieron resultados";
            Categorias.Add(newCategoria);
        }
    }
}
