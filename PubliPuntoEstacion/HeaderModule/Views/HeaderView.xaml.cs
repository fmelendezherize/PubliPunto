using Decktra.PubliPuntoEstacion.Library;
using System.Windows;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.HeaderModule.Views
{
    /// <summary>
    /// Interaction logic for HeaderUserControl.xaml
    /// </summary>
    public partial class HeaderView : UserControl
    {
        public HeaderView()
        {
            InitializeComponent();
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            GlobalCommands.GoToHomeCommand.Execute(null);
        }

        private void ButtonBusquedaCategoria_Click(object sender, RoutedEventArgs e)
        {
            GlobalCommands.GoToBusquedaCategoriaCommand.Execute(null);
        }

        private void ButtonBusquedaTeclado_Click(object sender, RoutedEventArgs e)
        {
            GlobalCommands.GoToBusquedaTecladoCommand.Execute(null);
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GlobalCommands.GoToHomeCommand.Execute(null);
        }
    }
}
