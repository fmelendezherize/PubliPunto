using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesLoginView.xaml
    /// </summary>
    public partial class CuponesLoginView : UserControl
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        public CuponesLoginView()
        {
            InitializeComponent();
        }

        private void ButtonEnviar_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.TextBoxEmail.Text))
            {
                var errorWnd = this.Container.Resolve<Views.CuponesView.ErrorMustLoginWindow>();
                errorWnd.OnNavigatedTo("ErrorLogin");
                errorWnd.Owner = Application.Current.MainWindow;
                errorWnd.ShowDialog();
                return;
            }

            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesAutorizadoView", UriKind.Relative));
        }

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
        }
    }
}
