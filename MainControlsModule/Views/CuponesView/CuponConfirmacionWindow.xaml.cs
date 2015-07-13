using System.Windows;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponSuccessWindow.xaml
    /// </summary>
    public partial class CuponConfirmacionWindow : Window
    {
        public CuponConfirmacionWindow()
        {
            InitializeComponent();
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonConfirmar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
