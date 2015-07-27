using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    class BusquedaCategoriaViewModel : BindableBase
    {
        private readonly RamoComercialRepository _ramoComercialRepository;
        private readonly EnteComercialRepository _enteComercialRepository;

        Categoria _categoriaRequested;
        public Categoria CategoriaRequested
        {
            get { return _categoriaRequested; }
            set { SetProperty(ref _categoriaRequested, value, "CategoriaRequested"); }
        }

        public ObservableCollection<Categoria> ListOfCategoriasRequested { get; set; }
        public ICommand ShowCategoriasByLetterCommand { get; set; }
        public ICommand ShowEnteComercialsCommand { get; set; }

        public BusquedaCategoriaViewModel()
        {
            _ramoComercialRepository = new RamoComercialRepository();
            _enteComercialRepository = new EnteComercialRepository();

            ShowCategoriasByLetterCommand = new DelegateCommand<string>(this.ShowCategoriasByLetter);
            ShowEnteComercialsCommand = new DelegateCommand<object>(this.ShowEnteComercials);

            ListOfCategoriasRequested = new ObservableCollection<Categoria>();
        }

        public void Init()
        {
            ListOfCategoriasRequested.Clear();
            this.ShowCategoriasAll();
        }

        private void ShowCategoriasByLetter(string letter)
        {
            ListOfCategoriasRequested.Clear();
            AddCategoriaToList(letter);
        }

        private void ShowCategoriasAll()
        {
            ListOfCategoriasRequested.Clear();
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
            //RaisePropertyChanged(() => this.ListOfCategoriasRequested);
        }

        private void AddCategoriaToList(string letter)
        {
            CategoriaRequested = new Categoria
            {
                NombreCategoria = letter
            };

            CategoriaRequested.ListOfSubCategorias = (from q in _ramoComercialRepository.GetAllByFirstLetter(letter)
                                                      select new SubCategoria
                                                      {
                                                          Nombre = q.Nombre,
                                                          Id = q.Id,
                                                          LogoUrl = q.LogoUrl,
                                                          TipoSubCategoria = TipoSubCategoria.RamoComercial
                                                      }).ToList();

            if (CategoriaRequested.ListOfSubCategorias.Count > 0) ListOfCategoriasRequested.Add(CategoriaRequested);
        }

        public void ShowEnteComercials(object idRamoComercial)
        {
            var ramoComercial = _ramoComercialRepository.FindBy(int.Parse(idRamoComercial.ToString()));
            CategoriaRequested = new Categoria
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

            ListOfCategoriasRequested.Clear();
            if (CategoriaRequested.ListOfSubCategorias.Count > 0) ListOfCategoriasRequested.Add(CategoriaRequested);
        }
    }
}
