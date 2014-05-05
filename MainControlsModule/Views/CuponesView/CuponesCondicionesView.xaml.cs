using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesCondiciones.xaml
    /// </summary>
    public partial class CuponesCondicionesView : UserControl, INavigationAware
    {
        public CuponesCondicionesView()
        {
            InitializeComponent();
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
            this.DataContext = navigationContext.NavigationService.Region.Context;
        }
    }
}
