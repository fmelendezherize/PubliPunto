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