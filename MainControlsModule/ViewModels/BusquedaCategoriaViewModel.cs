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
        public ObservableCollection<Categoria> Categorias { get; set; }
        private readonly RamoComercialRepository _ramoComercialRepository;
        private readonly EnteComercialRepository _enteComercialRepository;
        public ICommand ShowCategoriasByLetterCommand { get; set; }
        public ICommand ShowEnteComercialsCommand { get; set; }

        public BusquedaCategoriaViewModel()
        {
            _ramoComercialRepository = new RamoComercialRepository();
            _enteComercialRepository = new EnteComercialRepository();
            ShowCategoriasByLetterCommand = new DelegateCommand<string>(this.ShowCategoriasByLetter);
            ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);
            Categorias = new ObservableCollection<Categoria>();
        }

        public void Init()
        {
            Categorias.Clear();
            this.ShowCategoriasAll();
        }

        private void ShowCategoriasByLetter(string letter)
        {
            Categorias.Clear();
            AddCategoriaToList(letter);
        }

        private void ShowCategoriasAll()
        {
            Categorias.Clear();
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
            RaisePropertyChanged(() => this.Categorias);
        }

        private void AddCategoriaToList(string letter)
        {
            var newCategoria = new Categoria
                        {
                            NombreCategoria = letter,
                            ListCategorias = (from q in _ramoComercialRepository.GetAllByFirstLetter(letter)
                                              select new SubCategoria
                                              {
                                                  Nombre = q.Nombre,
                                                  Id = q.Id,
                                                  TipoSubCategoria = TipoSubCategoria.RamoComercial
                                              }).ToList()
                        };
            if (newCategoria.ListCategorias.Count > 0) Categorias.Add(newCategoria);
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
            Categorias.Clear();
            if (newCategoria.ListCategorias.Count > 0) Categorias.Add(newCategoria);
            RaisePropertyChanged(() => this.Categorias);
        }
    }
}
