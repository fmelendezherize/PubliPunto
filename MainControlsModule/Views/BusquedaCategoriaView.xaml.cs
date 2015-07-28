using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
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

        [Dependency]
        public ILoggerFacade Logger { get; set; }

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

        private void SelectSubCategoria(SubCategoria selectedSubCategoria)
        {
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

        private void CategoryItemControl_OnCategoriaSelected(object sender, EventArgs e)
        {
            SelectedSubCategoria = sender as SubCategoria;
            SelectSubCategoria(SelectedSubCategoria);
        }

        private void ButtonCategoriaPorLetra_Click(object sender, System.Windows.RoutedEventArgs e)
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

        private Point LastPosition;
        private SubCategoria SelectedSubCategoria;
        private bool TouchMoved;

        private void ListViewCategorias_TouchDown(object sender, TouchEventArgs e)
        {
            LastPosition = e.GetTouchPoint(this.TextBlockTitulo).Position;
        }

        private void ListViewCategorias_TouchUp(object sender, TouchEventArgs e)
        {
            if (!TouchMoved)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ListViewCategorias_TouchMove(object sender, TouchEventArgs e)
        {
            MoverScrollByTouch(e);
        }

        private void MoverScrollByTouch(TouchEventArgs e)
        {
            var actualPosition = e.GetTouchPoint(this.TextBlockTitulo).Position;

            var diffPosition = actualPosition.Y - LastPosition.Y;
            if ((diffPosition > -10) & (diffPosition < 10))
            {
                TouchMoved = false;
                return;
            };

            double offset = this.ScrollViewerCategorias.VerticalOffset - (diffPosition / 10);
            if (actualPosition.Y > LastPosition.Y)
            {
                this.ScrollViewerCategorias.ScrollToVerticalOffset(offset);
                TouchMoved = true;
                e.Handled = true;
            }
            else
            {
                this.ScrollViewerCategorias.ScrollToVerticalOffset(offset);
                TouchMoved = true;
                e.Handled = true;
            }
        }
    }
}