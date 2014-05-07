using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for BusquedaTecladoView.xaml
    /// </summary>
    public partial class BusquedaTecladoView : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        private IRegionNavigationService navigationService;

        public BusquedaTecladoView()
        {
            this.InitializeComponent();
            this.touchKeyboard.SetControlToWriteAlphaNumeric(this.TextBoxSearch);
            this.touchKeyboard.OnButtonClick += touchKeyboard_OnButtonClick;
        }

        private void touchKeyboard_OnButtonClick(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                UriQuery query = new UriQuery();
                query.Add("criterio", this.TextBoxSearch.Text);
                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("BusquedaTecladoView" + query.ToString(), UriKind.Relative));
            }
        }

        private void ButtonSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UriQuery query = new UriQuery();
            query.Add("criterio", this.TextBoxSearch.Text);
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("BusquedaTecladoView" + query.ToString(), UriKind.Relative));
        }


        private void CategoryItemControl_OnListViewCategoriaMouseClick(object sender, System.EventArgs e)
        {
            if (IsPressed) return;

            var selectedSubCategoria = sender as SubCategoria;
            if (selectedSubCategoria != null)
            {
                UriQuery query = new UriQuery();
                query.Add("ID", selectedSubCategoria.Id.ToString());
                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("DatosClienteView" + query.ToString(), UriKind.Relative));
            }
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

            if (!String.IsNullOrEmpty(navigationContext.Parameters["criterio"]))
            {
                this.TextBoxSearch.Text = navigationContext.Parameters["criterio"];
                ((BusquedaTecladoViewModel)this.DataContext).SearchEnteComercialsCommand.
                    Execute(navigationContext.Parameters["criterio"]);
                return;
            }

            if (!String.IsNullOrEmpty(navigationContext.Parameters["reset"]))
            {
                if (navigationContext.Parameters["reset"] == "true")
                {
                    this.TextBoxSearch.Clear();
                    ((BusquedaTecladoViewModel)this.DataContext).Init();
                    return;
                }
            }
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
        private void ListViewCategorias_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InitY = e.GetPosition(TextBoxSearch).Y;
        }

        private void ListViewCategorias_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsPressed)
            {
                IsPressed = false;
            }
        }

        private void ListViewCategorias_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsPressed = true;
                const double DBL_Offtset = .6;
                if (e.GetPosition(TextBoxSearch).Y > InitY)
                {
                    //Bajo
                    this.ScrollViewerCategorias.ScrollToVerticalOffset(this.ScrollViewerCategorias.VerticalOffset + DBL_Offtset);
                    InitY = e.GetPosition(TextBoxSearch).Y - DBL_Offtset;
                }
                else
                {
                    //Subio
                    this.ScrollViewerCategorias.ScrollToVerticalOffset(this.ScrollViewerCategorias.VerticalOffset - DBL_Offtset);
                    InitY = e.GetPosition(TextBoxSearch).Y + DBL_Offtset;
                }

            }
        }
    }
}