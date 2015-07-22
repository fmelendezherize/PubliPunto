using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Windows;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public DialogWindow()
        {
            InitializeComponent();
        }

        public void OnNavigatedTo(string accion)
        {
            if (accion == "ErrorFormularioLibre")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Debe llenar por completo el formulario. Por favor revise los campos solicitados.";
            }
            else if (accion == "ErrorLogin")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Sus datos de identificación no son correctos. Por favor revise los campos solicitados.";
            }
            else if (accion == "ErrorPromocion")
            {
                this.TextBlockTitulo.Text = "Disculpe !";
                this.TextBlockMensaje.Text = "Promoción no disponible o ya terminada.";
            }
        }
    }
}
