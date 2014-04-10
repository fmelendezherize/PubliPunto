using Microsoft.Practices.Unity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesAutorizadoView.xaml
    /// </summary>
    public partial class CuponesAutorizadoView : UserControl
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public CuponesAutorizadoView()
        {
            InitializeComponent();
        }

        private void TextBlockReclamarCupon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var errorWnd = this.Container.Resolve<Views.CuponesView.CuponSuccessWindow>();
            errorWnd.Owner = Application.Current.MainWindow;
            errorWnd.ShowDialog();
        }
    }
}
