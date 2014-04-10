using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesInicioView.xaml
    /// </summary>
    public partial class CuponesInicioView : UserControl
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        public CuponesInicioView()
        {
            InitializeComponent();
        }

        private void ImageFacebook_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }

        private void TextBlockReclamarCupon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var errorWnd = this.Container.Resolve<Views.CuponesView.ErrorMustLoginWindow>();
            errorWnd.OnNavigatedTo("MustLogin");
            errorWnd.Owner = Application.Current.MainWindow;
            errorWnd.ShowDialog();
        }
    }
}
