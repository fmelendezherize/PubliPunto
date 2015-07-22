using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for ContactanosView.xaml
    /// </summary>
    public partial class ContactanosClientesView : UserControl, INavigationAware
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private IRegionNavigationService navigationService;
        private Control PreviousTextBox;

        public ContactanosClientesView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PreviousTextBox != null)
            {
                this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DEDEE6"));
            }
            this.PreviousTextBox = sender as TextBox;
            this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#b1b1b8"));
            this.touchKeyboard.SetControlToWriteAlphaNumeric(this.PreviousTextBox);
        }

        private void TextBoxNumeric_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PreviousTextBox != null)
            {
                this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DEDEE6"));
            }
            this.PreviousTextBox = sender as Control;
            this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#b1b1b8"));
            this.touchKeyboard.SetControlToWriteNumeric(this.PreviousTextBox);
        }

        private void TextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //nada
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.navigationService = navigationContext.NavigationService;

            this.TextBoxEmail.Clear();
            this.TextBoxNombre.Clear();
            this.TextBoxTelefono.Clear();
            this.TextBoxComentario.Clear();
            this.TextBoxTelefonoPrefix.Clear();

            this.FormularioPanel.Visibility = System.Windows.Visibility.Visible;
            this.FormularioEnviadoPanel.Visibility = System.Windows.Visibility.Collapsed;
            this.touchKeyboard.Visibility = System.Windows.Visibility.Visible;

            this.TextBoxNombre_GotFocus(this.TextBoxNombre, null);
        }

        private bool IsDatosContactanosValidos()
        {
            return false;
        }

        private void ButtonEnviar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsDatosContactanosValidos())
            {
                this.FormularioPanel.Visibility = System.Windows.Visibility.Collapsed;
                this.FormularioEnviadoPanel.Visibility = System.Windows.Visibility.Visible;
                this.touchKeyboard.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                var errorWnd = this.Container.Resolve<Views.DialogWindow>();
                errorWnd.OnNavigatedTo("ErrorFormularioLibre");
                errorWnd.Owner = Application.Current.MainWindow;
                errorWnd.MouseDown += errorWnd_MouseDown;
                errorWnd.ShowDialog();
            }
        }

        void errorWnd_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window wnd = sender as Window;
            wnd.Close();
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private void TextBoxNombre_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PreviousTextBox != null)
            {
                this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DEDEE6"));
            }
            this.PreviousTextBox = sender as TextBox;
            this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#b1b1b8"));
            this.touchKeyboard.SetControlToWriteCharacter(this.PreviousTextBox);
        }

        private void TextBoxNewNombreApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            var caret = this.TextBoxNombre.CaretIndex;
            this.TextBoxNombre.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.TextBoxNombre.Text.ToLower());
            this.TextBoxNombre.CaretIndex = caret;
        }
    }
}
