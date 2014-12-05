using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesCondiciones.xaml
    /// </summary>
    public partial class CuponesCondicionesView : UserControl, INavigationAware
    {
        private IRegionNavigationService navigationService;

        [Dependency]
        public IRegionManager RegionManager { get; set; }

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
            this.DataContext = null;
            this.DataContext = navigationContext.NavigationService.Region.Context;
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private void TextBlockReclamarCupon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }
    }
}
