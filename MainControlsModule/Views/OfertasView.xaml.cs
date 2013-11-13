using System.Windows;
using System.Windows.Controls;
using Decktra.PubliPuntoEstacion.Library;

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
	        GlobalCommands.GoToDatosClienteCommand.Execute(null);
	    }
	}
}