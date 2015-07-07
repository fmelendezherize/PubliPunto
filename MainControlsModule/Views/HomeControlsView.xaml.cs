using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for HomeControlsView.xaml
    /// </summary>
    public partial class HomeControlsView : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

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
            NuestrosClientesView ncvm = this.RegionManager.Regions[RegionNames.REGION_NUESTROSCLIENTES_AREA].ActiveViews.
                FirstOrDefault() as NuestrosClientesView;
            if (ncvm != null) ncvm.ShowClientes();
            OfertasView ofvm = this.RegionManager.Regions[RegionNames.REGION_OFERTAS_AREA].ActiveViews.
                FirstOrDefault() as OfertasView;
            if (ofvm != null) ofvm.ShowOfertas();
        }
    }
}