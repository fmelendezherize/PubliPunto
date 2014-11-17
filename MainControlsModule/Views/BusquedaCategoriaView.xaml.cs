using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for BusquedaCategoriaView.xaml
    /// </summary>
    public partial class BusquedaCategoriaView : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        private IRegionNavigationService navigationService;

        public BusquedaCategoriaView()
        {
            this.InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //nada
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.navigationService = navigationContext.NavigationService;

            if ((navigationContext.Parameters["subCategoria"] != null) &&
                (!String.IsNullOrEmpty(navigationContext.Parameters["subCategoria"].ToString())))
            {
                ((BusquedaCategoriaViewModel)this.DataContext).ShowEnteComercialsCommand.Execute(
                    navigationContext.Parameters["subCategoria"].ToString());
                return;
            }

            if ((navigationContext.Parameters["criterioLetter"] != null) &&
                (!String.IsNullOrEmpty(navigationContext.Parameters["criterioLetter"].ToString())))
            {
                ((BusquedaCategoriaViewModel)this.DataContext).ShowCategoriasByLetterCommand.Execute(
                    navigationContext.Parameters["criterioLetter"].ToString());
                return;
            }

            if ((navigationContext.Parameters["reset"] != null) &&
                (!String.IsNullOrEmpty(navigationContext.Parameters["reset"].ToString())))
            {
                if (navigationContext.Parameters["reset"].ToString() == "true")
                {
                    ((BusquedaCategoriaViewModel)this.DataContext).Init();
                    return;
                }
            }
        }

        private void CategoryItemControl_OnOnListViewCategoriaMouseClick(object sender, EventArgs e)
        {
            if (IsPressed) return;

            var selectedSubCategoria = sender as SubCategoria;
            if (selectedSubCategoria != null)
            {
                if (selectedSubCategoria.TipoSubCategoria == TipoSubCategoria.RamoComercial)
                {
                    NavigationParameters query = new NavigationParameters();
                    query.Add("subCategoria", selectedSubCategoria.Id.ToString());
                    this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                        new Uri("BusquedaCategoriaView" + query.ToString(), UriKind.Relative));
                }
                else
                {
                    NavigationParameters query = new NavigationParameters();
                    query.Add("ID", selectedSubCategoria.Id.ToString());
                    this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                        new Uri("DatosClienteView" + query.ToString(), UriKind.Relative));
                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigationParameters query = new NavigationParameters();
            query.Add("criterioLetter", button.Content.ToString());
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("BusquedaCategoriaView" + query.ToString(), UriKind.Relative));
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private bool IsPressed = false;
        private double InitY;
        private void ListViewCategorias_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsPressed = true;
                const double DBL_Offtset = .6;
                if (e.GetPosition(TextBlockTitulo).Y > InitY)
                {
                    //Bajo
                    this.ScrollViewerCategorias.ScrollToVerticalOffset(this.ScrollViewerCategorias.VerticalOffset + DBL_Offtset);
                    InitY = e.GetPosition(TextBlockTitulo).Y - DBL_Offtset;
                }
                else
                {
                    //Subio
                    this.ScrollViewerCategorias.ScrollToVerticalOffset(this.ScrollViewerCategorias.VerticalOffset - DBL_Offtset);
                    InitY = e.GetPosition(TextBlockTitulo).Y + DBL_Offtset;
                }
            }
        }

        private void ListViewCategorias_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsPressed)
            {
                IsPressed = false;
            }
        }

        private void ListViewCategorias_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InitY = e.GetPosition(TextBlockTitulo).Y;
        }
    }
}