using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponSuccessWindow.xaml
    /// </summary>
    public partial class CuponCondicionesUsoWindow : Window
    {
        public CuponCondicionesUsoWindow(string condicionesUso)
        {
            InitializeComponent();

            this.TextBlockCondiciones.Text = condicionesUso;
            ReadContrato();
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

        private void ReadContrato()
        {
            string nombreContrato = "contrato.txt";
            if (File.Exists(nombreContrato))
            {
                this.TextBlockPoliticas.Text = File.ReadAllText(nombreContrato);
            }
        }

        private Point LastPosition;
        private bool TouchMoved;
        private const short DIFF_POSITION_CONST = 5;

        private void ScrollViewer_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (!TouchMoved)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ScrollViewer_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            LastPosition = e.GetTouchPoint(this.TextBlockTitulo).Position;
        }

        private void ScrollViewer_TouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            MoverScrollByTouch(e);
        }

        private void MoverScrollByTouch(TouchEventArgs e)
        {
            var actualPosition = e.GetTouchPoint(this.TextBlockTitulo).Position;

            var diffPosition = actualPosition.Y - LastPosition.Y;
            if ((diffPosition > (DIFF_POSITION_CONST * -1)) & (diffPosition < DIFF_POSITION_CONST))
            {
                TouchMoved = false;
                return;
            };

            double offset = this.ScrollViewerTexto.VerticalOffset - (diffPosition / DIFF_POSITION_CONST);
            if (actualPosition.Y > LastPosition.Y)
            {
                this.ScrollViewerTexto.ScrollToVerticalOffset(offset);
                TouchMoved = true;
                e.Handled = true;
            }
            else
            {
                this.ScrollViewerTexto.ScrollToVerticalOffset(offset);
                TouchMoved = true;
                e.Handled = true;
            }
        }
    }
}
