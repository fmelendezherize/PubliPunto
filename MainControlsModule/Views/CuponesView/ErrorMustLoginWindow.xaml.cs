using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Windows;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for ErrorMustLoginWindow.xaml
    /// </summary>
    public partial class ErrorMustLoginWindow : Window
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public ErrorMustLoginWindow()
        {
            InitializeComponent();
        }

        public void OnNavigatedTo(string accion)
        {
            if (accion == "ErrorFormularioLibre")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Debe llenar por completo el formulario para poder obtener un cupón a traves de este kiosko.";
            }
            else if (accion == "ErrorLogin")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Su cédula de identidad o PIN son incorrectos. Por favor vuelva a introducirlos.";
            }
            else if (accion == "ErrorPromocion")
            {
                this.TextBlockTitulo.Text = "Disculpe !";
                this.TextBlockMensaje.Text = "Promoción no disponible o ya terminada.";
            }
        }
    }
}
