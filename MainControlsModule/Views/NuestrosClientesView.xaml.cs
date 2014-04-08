using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Library;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for NuestrosClientesView.xaml
    /// </summary>
    public partial class NuestrosClientesView : UserControl
    {
        public NuestrosClientesView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            EnteComercial ente = button.DataContext as EnteComercial;
            if (ente != null) GlobalCommands.GoToDatosClienteCommand.Execute(ente.Id);

            //this.WrapPanelClientes.Visibility = System.Windows.Visibility.Collapsed;
            //this.WrapPanelVideo.Visibility = System.Windows.Visibility.Visible;
            //this.Video.Play();
        }

        public void StopVideo()
        {
            //this.Video.Stop();
        }
    }
}
