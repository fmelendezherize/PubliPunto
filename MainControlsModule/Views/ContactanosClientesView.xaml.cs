using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views
{
    /// <summary>
    /// Interaction logic for ContactanosView.xaml
    /// </summary>
    public partial class ContactanosClientesView : UserControl, INavigationAware
    {
        private IRegionNavigationService navigationService;
        private TextBox PreviousTextBox;

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

            this.FormularioPanel.Visibility = System.Windows.Visibility.Visible;
            this.FormularioEnviadoPanel.Visibility = System.Windows.Visibility.Collapsed;
            this.touchKeyboard.Visibility = System.Windows.Visibility.Visible;

            this.TextBox_GotFocus(this.TextBoxNombre, null);
        }

        private void ButtonEnviar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.FormularioPanel.Visibility = System.Windows.Visibility.Collapsed;
            this.FormularioEnviadoPanel.Visibility = System.Windows.Visibility.Visible;
            this.touchKeyboard.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }
    }
}
