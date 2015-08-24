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
            this.AutoCompleteBoxSearch.Populating += AutoCompleteBoxSearch_Populating;
            this.TextBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            this.AutoCompleteBoxSearch.SelectionChanged += AutoCompleteBoxSearch_SelectionChanged;
        }

        void AutoCompleteBoxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.TextBoxSearch.Text = this.AutoCompleteBoxSearch.Text;
            this.TextBoxSearch.CaretIndex = this.TextBoxSearch.Text.Length;
            this.ButtonSearch_Click(this, null);
        }

        void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.AutoCompleteBoxSearch.Text = this.TextBoxSearch.Text;
            if (this.AutoCompleteBoxSearch.Text.Length > 0)
            {
                this.AutoCompleteBoxSearch.PopulateComplete();
                this.AutoCompleteBoxSearch.IsDropDownOpen = true;
            }
        }

        void AutoCompleteBoxSearch_Populating(object sender, PopulatingEventArgs e)
        {

        }

        private void touchKeyboard_OnButtonClick(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.AutoCompleteBoxSearch.IsDropDownOpen = false;
                NavigationParameters query = new NavigationParameters();
                query.Add("criterio", this.TextBoxSearch.Text);
                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("BusquedaTecladoView" + query.ToString(), UriKind.Relative));
            }
        }

        private void ButtonSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AutoCompleteBoxSearch.IsDropDownOpen = false;
            NavigationParameters query = new NavigationParameters();
            query.Add("criterio", this.TextBoxSearch.Text);
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("BusquedaTecladoView" + query.ToString(), UriKind.Relative));
        }


        private void CategoryItemControl_OnListViewCategoriaMouseClick(object sender, System.EventArgs e)
        {
            var selectedSubCategoria = sender as SubCategoria;
            if (selectedSubCategoria != null)
            {
                NavigationParameters query = new NavigationParameters();
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

            if ((navigationContext.Parameters["criterio"] != null) &&
                (!String.IsNullOrEmpty(navigationContext.Parameters["criterio"].ToString())))
            {
                this.TextBoxSearch.Text = navigationContext.Parameters["criterio"].ToString();
                ((BusquedaTecladoViewModel)this.DataContext).SearchEnteComercialsCommand.
                    Execute(navigationContext.Parameters["criterio"]);
                return;
            }

            if ((navigationContext.Parameters["reset"] != null) &&
                (!String.IsNullOrEmpty(navigationContext.Parameters["reset"].ToString())))
            {
                if (navigationContext.Parameters["reset"].ToString() == "true")
                {
                    this.TextBoxSearch.Text = "";
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

        private System.Windows.Point LastPosition;
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