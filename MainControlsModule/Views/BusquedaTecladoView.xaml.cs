using Decktra.PubliPuntoEstacion.Library;
using Decktra.PubliPuntoEstacion.MainControlsModule.Models;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

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
    }
}