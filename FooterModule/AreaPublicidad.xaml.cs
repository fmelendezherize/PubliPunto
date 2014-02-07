using Decktra.PubliPuntoEstacion.Library;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.FooterModule
{
    /// <summary>
    /// Interaction logic for AreaPublicidad.xaml
    /// </summary>
    public partial class AreaPublicidad : UserControl
    {
        public AreaPublicidad()
        {
            this.InitializeComponent();
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            GlobalCommands.GoToContactanosCommand.Execute(null);
        }
    }
}