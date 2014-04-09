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
            //this.ImageEnteComercial.Visibility = System.Windows.Visibility.Visible;
            string ID = navigationContext.Parameters["ID"];
            ((DatosClienteViewModel)DataContext).ShowEnteComercialCommand.Execute(ID);
        }

        private void ButtonReclamarCupon_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesInicioView", UriKind.Relative));
        }
    }
}