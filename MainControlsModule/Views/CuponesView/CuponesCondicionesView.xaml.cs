using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesCondiciones.xaml
    /// </summary>
    public partial class CuponesCondicionesView : UserControl, INavigationAware
    {
        private IRegionNavigationService navigationService;

        public CuponesCondicionesView()
        {
            InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //if (navigationContext.NavigationService.Journal.CurrentEntry.Uri.OriginalString != "CuponesInicioView")
            //{
            //    return false;
            //}

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //if (navigationContext.Uri.OriginalString != "CuponesInicioView")
            //{
            //    navigationContext.NavigationService.Region.Context = null;
            //    return;
            //}
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.navigationService = navigationContext.NavigationService;
            this.DataContext = navigationContext.NavigationService.Region.Context;
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }
    }
}
