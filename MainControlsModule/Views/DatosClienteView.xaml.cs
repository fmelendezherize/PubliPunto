using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for DatosClienteView.xaml
    /// </summary>
    public partial class DatosClienteView : UserControl, INavigationAware
    {
        public DatosClienteView()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
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
            this.ImageEnteComercial.Visibility = System.Windows.Visibility.Visible;
            string ID = navigationContext.Parameters["ID"];
            ((DatosClienteViewModel)DataContext).ShowEnteComercialCommand.Execute(ID);
        }
    }
}