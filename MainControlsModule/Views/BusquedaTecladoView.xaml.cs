using Decktra.PubliPuntoEstacion.Library;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for BusquedaTecladoView.xaml
    /// </summary>
    public partial class BusquedaTecladoView : UserControl, INavigationAware
    {
        public BusquedaTecladoView()
        {
            this.InitializeComponent();
            this.touchKeyboard.SetControlToWrite(this.TextBoxSearch);
            this.touchKeyboard.OnButtonClick += touchKeyboard_OnButtonClick;
        }

        private void touchKeyboard_OnButtonClick(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ((BusquedaTecladoViewModel)this.DataContext).SearchEnteComercialsCommand.Execute(this.TextBoxSearch.Text);
            }
        }

        private void CategoryItemControl_OnListViewCategoriaMouseClick(object sender, System.EventArgs e)
        {
            if (IsPressed) return;

            var selectedSubCategoria = sender as SubCategoria;
            if (selectedSubCategoria != null)
            {
                GlobalCommands.GoToDatosClienteCommand.Execute(selectedSubCategoria.Id);
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
            this.TextBoxSearch.Clear();
            ((BusquedaTecladoViewModel)this.DataContext).Init();
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.TextBoxSearch.Clear();
            ((BusquedaTecladoViewModel)this.DataContext).BackCommand.Execute(null);
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