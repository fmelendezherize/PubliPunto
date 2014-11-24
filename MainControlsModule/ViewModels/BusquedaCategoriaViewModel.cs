using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
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

        public BusquedaCategoriaViewModel()
        {
            _ramoComercialRepository = new RamoComercialRepository();
            _enteComercialRepository = new EnteComercialRepository();

            ShowCategoriasByLetterCommand = new DelegateCommand<string>(this.ShowCategoriasByLetter);
            ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);

            CategoriaActual = new ObservableCollection<Categoria>();
        }

        public void Init()
        {
            CategoriaActual.Clear();
            this.ShowCategoriasAll();
        }

        private void ShowCategoriasByLetter(string letter)
        {
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

            newCategoria.ListOfSubCategorias = (from q in _ramoComercialRepository.GetAllByFirstLetter(letter)
                                                select new SubCategoria
                                                {
                                                    Nombre = q.Nombre,
                                                    Id = q.Id,
                                                    LogoUrl = q.LogoUrl,
                                                    TipoSubCategoria = TipoSubCategoria.RamoComercial
                                                }).ToList();

            if (newCategoria.ListOfSubCategorias.Count > 0) CategoriaActual.Add(newCategoria);
        }

        public void ShowEnteComercials(object idRamoComercial)
        {
            var ramoComercial = _ramoComercialRepository.FindBy(int.Parse(idRamoComercial.ToString()));
            var newCategoria = new Categoria
            {
                NombreCategoria = ramoComercial.Nombre,
                LogoUrl = ramoComercial.LogoUrl,
                ListOfSubCategorias = (from q in _enteComercialRepository.GetEnteComercialsBy(int.Parse(idRamoComercial.ToString()))
                                       select new SubCategoria()
                                       {
                                           Id = q.Id,
                                           Nombre = q.Nombre,
                                           LogoUrl = q.LogoUrl,
                                           TipoSubCategoria = TipoSubCategoria.EnteComercial
                                       }).ToList()
            };

            CategoriaActual.Clear();
            if (newCategoria.ListOfSubCategorias.Count > 0) CategoriaActual.Add(newCategoria);
            RaisePropertyChanged(() => this.CategoriaActual);
        }
    }
}
