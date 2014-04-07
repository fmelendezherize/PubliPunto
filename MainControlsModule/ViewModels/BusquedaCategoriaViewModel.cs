using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.Library;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    class BusquedaCategoriaViewModel : NotificationObject
    {
        private readonly RamoComercialRepository _ramoComercialRepository;
        private readonly EnteComercialRepository _enteComercialRepository;

        public ObservableCollection<Categoria> CategoriaActual { get; set; }
        public ICommand ShowCategoriasByLetterCommand { get; set; }
        public ICommand ShowEnteComercialsCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private List<Categoria> _categoriaAnterior;

        public BusquedaCategoriaViewModel()
        {
            _ramoComercialRepository = new RamoComercialRepository();
            _enteComercialRepository = new EnteComercialRepository();

            ShowCategoriasByLetterCommand = new DelegateCommand<string>(this.ShowCategoriasByLetter);
            ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);
            BackCommand = new DelegateCommand<object>(this.Back);

            CategoriaActual = new ObservableCollection<Categoria>();
        }

        public void Init()
        {
            this._categoriaAnterior = null;
            CategoriaActual.Clear();
            this.ShowCategoriasAll();
        }

        public bool CanExecuteBack(object arg)
        {
            if (this._categoriaAnterior == null) return false;
            return true;
        }

        public void Back(object arg)
        {
            if (this._categoriaAnterior != null)
            {
                CategoriaActual.Clear();
                foreach (Categoria item in this._categoriaAnterior)
                {
                    CategoriaActual.Add(item);
                }
                this._categoriaAnterior = null;
            }
            else
            {
                GlobalCommands.GoToHomeCommand.Execute(null);
            }
        }

        private void ShowCategoriasByLetter(string letter)
        {
            if (CategoriaActual.Count == 0)
            {
                this._categoriaAnterior = null;
            }
            else
            {
                this._categoriaAnterior = new List<Categoria>(CategoriaActual.ToList());
            }
            CategoriaActual.Clear();
            AddCategoriaToList(letter);
        }

        private void ShowCategoriasAll()
        {
            CategoriaActual.Clear();
            AddCategoriaToList("A");
            AddCategoriaToList("B");
            AddCategoriaToList("C");
            AddCategoriaToList("D");
            AddCategoriaToList("E");
            AddCategoriaToList("F");
            AddCategoriaToList("G");
            AddCategoriaToList("H");
            AddCategoriaToList("I");
            AddCategoriaToList("J");
            AddCategoriaToList("K");
            AddCategoriaToList("L");
            AddCategoriaToList("M");
            AddCategoriaToList("N");
            AddCategoriaToList("O");
            AddCategoriaToList("P");
            AddCategoriaToList("Q");
            AddCategoriaToList("R");
            AddCategoriaToList("S");
            AddCategoriaToList("T");
            AddCategoriaToList("U");
            AddCategoriaToList("V");
            AddCategoriaToList("W");
            AddCategoriaToList("X");
            AddCategoriaToList("Y");
            AddCategoriaToList("Z");
            RaisePropertyChanged(() => this.CategoriaActual);
        }

        private void AddCategoriaToList(string letter)
        {
            var newCategoria = new Categoria
                        {
                            NombreCategoria = letter
                        };

            newCategoria.ListCategorias = (from q in _ramoComercialRepository.GetAllByFirstLetter(letter)
                                           select new SubCategoria
                                           {
                                               Nombre = q.Nombre,
                                               Id = q.Id,
                                               TipoSubCategoria = TipoSubCategoria.RamoComercial
                                           }).ToList();

            if (newCategoria.ListCategorias.Count > 0) CategoriaActual.Add(newCategoria);
            //CategoriaActual.Add(newCategoria);
        }

        public void ShowEnteComercials(object idRamoComercial)
        {
            var ramoComercial = _ramoComercialRepository.FindBy((int)idRamoComercial);
            var newCategoria = new Categoria
            {
                NombreCategoria = ramoComercial.Nombre,
                ListCategorias = (from q in _enteComercialRepository.GetEnteComercialsBy((int)idRamoComercial)
                                  select new SubCategoria()
                                  {
                                      Id = q.Id,
                                      Nombre = q.Nombre,
                                      LogoUrl = q.LogoUrl,
                                      TipoSubCategoria = TipoSubCategoria.EnteComercial
                                  }).ToList()
            };

            this._categoriaAnterior = new List<Categoria>(CategoriaActual.ToList());
            CategoriaActual.Clear();

            if (newCategoria.ListCategorias.Count > 0) CategoriaActual.Add(newCategoria);
            RaisePropertyChanged(() => this.CategoriaActual);
        }
    }
}
