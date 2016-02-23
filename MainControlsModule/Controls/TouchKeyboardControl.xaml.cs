using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Controls
{
    /// <summary>
    /// Interaction logic for TouchKeyboardControl.xaml
    /// </summary>
    public partial class TouchKeyboardControl : UserControl
    {
        public delegate void OnButtonClickHandler(object sender, KeyEventArgs e);
        public event OnButtonClickHandler OnButtonClick;

        private Control _activeControl;

        private bool IsShiftEnabled { get; set; }
        private bool IsSymbEnabled { get; set; }

        private System.Windows.Input.KeyConverter keyConverter;

        public TouchKeyboardControl()
        {
            this.InitializeComponent();
            keyConverter = new System.Windows.Input.KeyConverter();
        }

        public void SetControlToWriteAlphaNumeric(Control control)
        {
            this.WrapPanelNumeric.Visibility = Visibility.Collapsed;
            if (this.IsSymbEnabled)
            {
                this.StackPanelAlpha1.Visibility = Visibility.Collapsed;
                this.StackPanelAlpha2.Visibility = Visibility.Collapsed;
                this.StackPanelAlpha3.Visibility = Visibility.Collapsed;

                this.StackPanelNumeric.Visibility = Visibility.Visible;
                this.StackPanelSymb1.Visibility = Visibility.Visible;
                this.StackPanelSymb2.Visibility = Visibility.Visible;
            }
            else
            {
                this.StackPanelAlpha1.Visibility = Visibility.Visible;
                this.StackPanelAlpha2.Visibility = Visibility.Visible;
                this.StackPanelAlpha3.Visibility = Visibility.Visible;

                this.StackPanelNumeric.Visibility = Visibility.Collapsed;
                this.StackPanelSymb1.Visibility = Visibility.Collapsed;
                this.StackPanelSymb2.Visibility = Visibility.Collapsed;
            }
            this.StackPanelAlpha4.Visibility = Visibility.Visible;

            this.WrapPanelNumeric.Width = double.NaN;
            Style style = this.FindResource("KeyNumericButtonStyle") as Style;
            Style newStyle = new Style();
            newStyle.TargetType = typeof(Button);
            newStyle.BasedOn = style;
            string buttonSize = "90";
            newStyle.Setters.Add(new Setter(Button.WidthProperty, double.Parse(buttonSize)));
            newStyle.Setters.Add(new Setter(Button.HeightProperty, double.Parse(buttonSize)));

            foreach (var item in this.WrapPanelNumeric.Children)
            {
                Button button = item as Button;
                if (button == null) continue;
                button.Style = newStyle;
            }
            this.ButtonZero.Height = double.Parse(buttonSize);
            this.ButtonZero.Width = double.Parse(buttonSize);

            this.ButtonDeleteNumeric.Visibility = Visibility.Collapsed;
            this.ButtonSymb.Visibility = Visibility.Visible;
            this.ButtonLeftSpace.Visibility = Visibility.Visible;
            this.ButtonRightSpace.Visibility = Visibility.Visible;
            this.ButtonLeftEnter.Visibility = Visibility.Visible;

            this._activeControl = control;
        }

        public void SetControlToWriteNumeric(Control control)
        {
            this.WrapPanelNumeric.Visibility = Visibility.Visible;
            this.StackPanelAlpha1.Visibility = Visibility.Hidden;
            this.StackPanelAlpha2.Visibility = Visibility.Hidden;
            this.StackPanelAlpha3.Visibility = Visibility.Hidden;
            this.StackPanelAlpha4.Visibility = Visibility.Hidden;

            this.WrapPanelNumeric.Width = 289;

            Style style = this.FindResource("KeyNumericButtonStyle") as Style;
            Style newStyle = new Style();
            newStyle.TargetType = typeof(Button);
            newStyle.BasedOn = style;
            string buttonSize = "90";
            newStyle.Setters.Add(new Setter(Button.WidthProperty, double.Parse(buttonSize)));
            newStyle.Setters.Add(new Setter(Button.HeightProperty, double.Parse(buttonSize)));

            foreach (var item in this.WrapPanelNumeric.Children)
            {
                Button button = item as Button;
                if (button == null) continue;
                button.Style = newStyle;
            }
            this.ButtonZero.Width = 185;
            this.ButtonZero.Height = double.Parse(buttonSize);

            this.ButtonDeleteNumeric.Visibility = Visibility.Visible;
            this.ButtonDeleteNumeric.Width = double.Parse(buttonSize);
            this.ButtonDeleteNumeric.Height = double.Parse(buttonSize);

            this._activeControl = control;
        }

        public void SetControlToWriteCharacter(Control control)
        {
            this.SetControlToWriteAlphaNumeric(control);
            this.ButtonSymb.Visibility = Visibility.Hidden;
            this.ButtonLeftSpace.Visibility = Visibility.Hidden;
            this.ButtonRightSpace.Visibility = Visibility.Hidden;
            this.ButtonLeftEnter.Visibility = Visibility.Hidden;
        }

        private void AlphaNumericButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var activeButton = sender as Button;
            if (activeButton != null) WriteAlphaNumericToActiveControl(activeButton.Content.ToString());
        }

        private void WriteAlphaNumericToActiveControl(string key)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();

            var textEvent = new TextCompositionEventArgs(Keyboard.PrimaryDevice,
                new TextComposition(InputManager.Current, Keyboard.FocusedElement, key)) { RoutedEvent = TextInputEvent };
            InputManager.Current.ProcessInput(textEvent);

            Key theKey = (Key)keyConverter.ConvertFromString(key);
            if (Keyboard.PrimaryDevice.ActiveSource == null) return;
            var keyEvent = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, theKey)
            {
                RoutedEvent = Keyboard.KeyDownEvent
            };
            if (OnButtonClick != null) OnButtonClick(this, keyEvent);
        }

        private void ButtonSymb_Click(object sender, RoutedEventArgs e)
        {
            this.IsSymbEnabled = !this.IsSymbEnabled;
            if (this.IsSymbEnabled)
            {
                this.ButtonSymb.BorderBrush = Brushes.LightGreen;
                this.ButtonSymb.BorderThickness = new Thickness(3);

                this.StackPanelAlpha1.Visibility = Visibility.Collapsed;
                this.StackPanelAlpha2.Visibility = Visibility.Collapsed;
                this.StackPanelAlpha3.Visibility = Visibility.Collapsed;

                this.StackPanelNumeric.Visibility = Visibility.Visible;
                this.StackPanelSymb1.Visibility = Visibility.Visible;
                this.StackPanelSymb2.Visibility = Visibility.Visible;
            }
            else
            {
                this.ButtonSymb.BorderThickness = new Thickness(0);

                this.StackPanelAlpha1.Visibility = Visibility.Visible;
                this.StackPanelAlpha2.Visibility = Visibility.Visible;
                this.StackPanelAlpha3.Visibility = Visibility.Visible;

                this.StackPanelNumeric.Visibility = Visibility.Collapsed;
                this.StackPanelSymb1.Visibility = Visibility.Collapsed;
                this.StackPanelSymb2.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonPunto_Click(object sender, RoutedEventArgs e)
        {
            WriteAlphaNumericToActiveControl(".");
        }

        private void ButtonArroba_Click(object sender, RoutedEventArgs e)
        {
            WriteAlphaNumericToActiveControl("@");
        }

        private void ButtonComma_Click(object sender, RoutedEventArgs e)
        {
            WriteAlphaNumericToActiveControl(",");
        }

        private void ButtonGuion_Click(object sender, RoutedEventArgs e)
        {
            WriteAlphaNumericToActiveControl("-");
        }

        private void ButtonGuionPiso_Click(object sender, RoutedEventArgs e)
        {
            WriteAlphaNumericToActiveControl("_");
        }

        private void SendKeyToActiveControl(Key key)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();

            if (Keyboard.PrimaryDevice.ActiveSource == null) return;
            var keyEvent = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
            {
                RoutedEvent = Keyboard.KeyDownEvent
            };
            InputManager.Current.ProcessInput(keyEvent);
            if (OnButtonClick != null) OnButtonClick(this, keyEvent);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            SendKeyToActiveControl(Key.Back);
        }

        private void ButtonEspacio_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            SendKeyToActiveControl(Key.Space);
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (this._activeControl == null) return;
            this._activeControl.Focus();
            SendKeyToActiveControl(Key.Enter);
        }

        private void ButtonShift_Click(object sender, RoutedEventArgs e)
        {
            this.IsShiftEnabled = !this.IsShiftEnabled;
            if (this.IsShiftEnabled)
            {
                this.ButtonShift.BorderBrush = Brushes.LightGreen;
                this.ButtonShift.BorderThickness = new Thickness(3);
                foreach (var item in this.StackPanelAlpha1.Children)
                {
                    Button button = item as Button;
                    if ((button == null) || (button.Content.GetType() != typeof(string))) continue;
                    button.Content = button.Content.ToString().ToUpper();
                }
                foreach (var item in this.StackPanelAlpha2.Children)
                {
                    Button button = item as Button;
                    if ((button == null) || (button.Content.GetType() != typeof(string))) continue;
                    button.Content = button.Content.ToString().ToUpper();
                }
                foreach (var item in this.StackPanelAlpha3.Children)
                {
                    Button button = item as Button;
                    if ((button == null) || (button.Content.GetType() != typeof(string))) continue;
                    button.Content = button.Content.ToString().ToUpper();
                }
            }
            else
            {
                this.ButtonShift.BorderThickness = new Thickness(0);
                foreach (var item in this.StackPanelAlpha1.Children)
                {
                    Button button = item as Button;
                    if ((button == null) || (button.Content.GetType() != typeof(string))) continue;
                    button.Content = button.Content.ToString().ToLower();
                }
                foreach (var item in this.StackPanelAlpha2.Children)
                {
                    Button button = item as Button;
                    if ((button == null) || (button.Content.GetType() != typeof(string))) continue;
                    button.Content = button.Content.ToString().ToLower();
                }
                foreach (var item in this.StackPanelAlpha3.Children)
                {
                    Button button = item as Button;
                    if ((button == null) || (button.Content.GetType() != typeof(string))) continue;
                    button.Content = button.Content.ToString().ToLower();
                }
            }
        }
    }
}