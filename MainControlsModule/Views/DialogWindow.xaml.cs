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
            this.Deactivated += DialogWindow_Deactivated;
            this.MouseDown += DialogWindow_MouseDown;
        }

        void DialogWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Hide();
        }

        void DialogWindow_Deactivated(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        public void ShowErrorFormulario()
        {
            this.TextBlockTitulo.Text = "Error !";
            this.TextBlockMensaje.Text = "Debe llenar por completo el formulario. Por favor revise los campos solicitados.";
            this.Owner = Application.Current.MainWindow;
            this.Show();
        }

        public void ShowErrorDatosUsuario()
        {
            this.TextBlockTitulo.Text = "Error !";
            this.TextBlockMensaje.Text = "Sus datos de identificación no son correctos. Por favor revise los campos solicitados.";
            this.Owner = Application.Current.MainWindow;
            this.Show();
        }

        public void ShowErrorPromocion(string msg)
        {
            this.TextBlockTitulo.Text = "Disculpe !";
            if (string.IsNullOrEmpty(msg))
            {
                this.TextBlockMensaje.Text = "Promoción no disponible o terminada.";
            }
            else
            {
                this.TextBlockMensaje.Text = msg;
            }
            this.Owner = Application.Current.MainWindow;
            this.Show();
        }
    }
}
