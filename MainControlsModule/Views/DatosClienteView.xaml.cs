using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for DatosClienteView.xaml
    /// </summary>
    public partial class DatosClienteView : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        private IRegionNavigationService navigationService;

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
            if (navigationContext.Uri.OriginalString != "CuponesInicioView")
            {
                navigationContext.NavigationService.Region.Context = null;
                return;
            }

            navigationContext.NavigationService.Region.Context = this.DataContext;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string ID = navigationContext.Parameters["ID"];
            ((DatosClienteViewModel)DataContext).ShowEnteComercialCommand.Execute(ID);
            this.navigationService = navigationContext.NavigationService;
        }

        private void ButtonReclamarCupon_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesInicioView", UriKind.Relative));
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