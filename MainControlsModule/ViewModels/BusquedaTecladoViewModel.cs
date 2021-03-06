﻿
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    class BusquedaTecladoViewModel
    {
        private readonly EnteComercialRepository _enteComercialRepository;

        public ObservableCollection<Categoria> Categorias { get; set; }
        public ICommand SearchEnteComercialsCommand { get; set; }

        public IEnumerable<string> AutoCompleteItems { get; set; }

        public BusquedaTecladoViewModel()
        {
            this._enteComercialRepository = new EnteComercialRepository();
            this.SearchEnteComercialsCommand = new DelegateCommand<string>(this.SearchEnteComercials);
            Categorias = new ObservableCollection<Categoria>();

            //var testItems = new List<string>();
            //testItems.AddRange(new string[]{
            //    "January", "February" ,"March", "April", "May", "June", "July", "August", "September", "Octuber", "November", "December"
            //});

            AutoCompleteItems = this._enteComercialRepository.GetAutoCompleteTags();
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
                NombreCategoria = "Resultados de la búsqueda"
            };

            newCategoria.ListOfSubCategorias.AddRange(
                (from q in _enteComercialRepository.GetEnteComercialsByName(obj)
                 select new SubCategoria()
                 {
                     Id = q.Id,
                     Nombre = q.Nombre,
                     LogoUrl = q.LogoUrl,
                     TipoSubCategoria = TipoSubCategoria.EnteComercial
                 }).ToList());

            if (obj.Length >= 2)
            {
                newCategoria.ListOfSubCategorias.AddRange(
                    (from q in _enteComercialRepository.GetEnteComercialsByTags(obj)
                     select new SubCategoria()
                     {
                         Id = q.Id,
                         Nombre = q.Nombre,
                         LogoUrl = q.LogoUrl,
                         TipoSubCategoria = TipoSubCategoria.EnteComercial
                     }).ToList());
            }

            Categorias.Clear();
            if (newCategoria.ListOfSubCategorias.Count == 0) newCategoria.NombreCategoria = "No se consiguieron resultados";
            Categorias.Add(newCategoria);
        }
    }
}
