using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for OfertasView.xaml
    /// </summary>
    public partial class OfertasView : UserControl
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public OfertasView()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
            this.ShowOfertas();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            var promocion = (Promocion)activeButton.DataContext;
            if (promocion != null && promocion.EnteComercialId != 0)
            {
                NavigationParameters query = new NavigationParameters();
                query.Add("ID", promocion.EnteComercial.Id.ToString());
                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("DatosClienteView" + query.ToString(), UriKind.Relative));
            }
        }

        public void ShowOfertas()
        {
            ((OfertasViewModel)this.DataContext).ShowEnteComercialsCommand.Execute(null);
        }
    }
}