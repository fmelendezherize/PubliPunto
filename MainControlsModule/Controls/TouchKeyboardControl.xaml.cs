using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Controls
{
    /// <summary>
    /// Interaction logic for TouchKeyboardControl.xaml
    /// </summary>
    public partial class TouchKeyboardControl : UserControl
    {
        public TouchKeyboardControl()
        {
            this.InitializeComponent();
        }

        public delegate void OnButtonClickHandler(object sender, KeyEventArgs e);
        public event OnButtonClickHandler OnButtonClick;
        private Control _activeControl;
        private bool isShiftEnabled;

        private void AlphaNumericButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this._activeControl != null)
            {
                this._activeControl.Focus();
                var activeButton = sender as Button;
                if (activeButton != null) WriteAlphaNumericToActiveControl(activeButton.Content.ToString());
            }
        }

        public void SetControlToWrite(Control control)
        {
            this._activeControl = control;
        }

        private void WriteAlphaNumericToActiveControl(string key)
        {
            if (this.isShiftEnabled) key = key.ToUpper();
            var textEvent = new TextCompositionEventArgs(Keyboard.PrimaryDevice,
                new TextComposition(InputManager.Current, Keyboard.FocusedElement, key)) { RoutedEvent = TextInputEvent };
            InputManager.Current.ProcessInput(textEvent);
        }

        private void SendKeyToActiveControl(Key key)
        {
            if (Keyboard.PrimaryDevice.ActiveSource == null) return;
            var keyEvent = new KeyEventArgs(Keyboard.PrimaryDevice,
                Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = UIElement.KeyDownEvent };
            InputManager.Current.ProcessInput(keyEvent);
            if (OnButtonClick != null) OnButtonClick(this, keyEvent);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            SendKeyToActiveControl(Key.Back);
        }

        private void ButtonArroba_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            WriteAlphaNumericToActiveControl("@");
        }

        private void ButtonPunto_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            WriteAlphaNumericToActiveControl(".");
        }

        private void ButtonEspacio_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            SendKeyToActiveControl(Key.Space);
        }

        private void ButtonGuion_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            WriteAlphaNumericToActiveControl("-");
        }

        private void ButtonPiso_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            WriteAlphaNumericToActiveControl("_");
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            SendKeyToActiveControl(Key.Enter);
        }

        private void ButtonShift_Click(object sender, RoutedEventArgs e)
        {
            this.isShiftEnabled = !this.isShiftEnabled;
        }
    }
}