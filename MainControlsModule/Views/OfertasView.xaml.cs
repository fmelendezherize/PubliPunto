using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Library;
using System.Windows;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for OfertasView.xaml
    /// </summary>
    public partial class OfertasView : UserControl
    {
        public OfertasView()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            if (activeButton != null)
            {
                GlobalCommands.GoToDatosClienteCommand.Execute(((EnteComercial)activeButton.DataContext).Id);
            }
        }
    }
}