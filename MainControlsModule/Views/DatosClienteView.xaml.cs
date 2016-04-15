using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.KioskoServicesModule;
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

        [Dependency]
        public GotoHomeTimerService TimerService { get; set; }

        private IRegionNavigationService navigationService;

        public DatosClienteView(IUnityContainer container)
        {
            this.InitializeComponent();

            container.BuildUp((DatosClienteViewModel)this.DataContext);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            TimerService.Stop();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string ID = navigationContext.Parameters["ID"].ToString();
            ((DatosClienteViewModel)DataContext).ShowEnteComercialCommand.Execute(ID);
            this.navigationService = navigationContext.NavigationService;
            navigationContext.NavigationService.Region.Context = this.DataContext;

            TimerService.Start();
        }

        private void ButtonReclamarCupon_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DatosClienteViewModel vm = this.DataContext as DatosClienteViewModel;
            if (vm.PromocionesActivas.Count == 1)
            {
                vm.PromocionSelected = vm.PromocionesActivas[0];
                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("CuponesLoginView", UriKind.Relative));
                return;
            }

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

        private void ButtonContactanosCliente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("ContactanosClientesView?NombreEnteComercial=" + this.TextBlockNombreEnteComercial.Text, UriKind.Relative));
        }

    }
}