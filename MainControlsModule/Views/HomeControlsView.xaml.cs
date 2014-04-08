using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for HomeControlsView.xaml
    /// </summary>
    public partial class HomeControlsView : UserControl, INavigationAware
    {
        public HomeControlsView()
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
            ((NuestrosClientesViewModel)this.NuestrosClientesView.DataContext).ShowEnteComercialsCommand.Execute(null);
            ((OfertasViewModel)this.OfertasView.DataContext).ShowEnteComercialsCommand.Execute(null);
        }
    }
}