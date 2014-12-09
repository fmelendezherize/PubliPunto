using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesRegistro.xaml
    /// </summary>
    public partial class CuponesRegistroView : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        private IRegionNavigationService navigationService;

        private Control PreviousTextBox;

        public CuponesRegistroView()
        {
            InitializeComponent();
        }

        private void TextBoxAlpha_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PreviousTextBox != null)
            {
                this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DEDEE6"));
            }
            this.PreviousTextBox = sender as Control;
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

        private void ButtonRegistro_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsCedulaIdentidadValida() & IsEmailValido() & IsNombreApellidoValido())
            {
                string cedula = this.TextBoxNewCedulaIdentidad.Text;
                //TODO hablar con Nacho que valide esto
                //if (this.RadioButtonCedulaFirstLetter.IsChecked.Value)
                //{
                //    cedula = "V" + this.TextBoxCedulaIdentidad.Text;
                //}
                //else
                //{
                //    cedula = "E" + this.TextBoxCedulaIdentidad.Text;
                //};

                ((DatosClienteViewModel)this.DataContext).ReclamarCuponCommand.Execute(
                    new Usuario
                    {
                        Cedula = cedula,
                        Email = this.TextBoxNewEmail.Text,
                        Nombre = this.TextBoxNewNombreApellido.Text
                    });
                return;
            }

            var errorWnd = this.Container.Resolve<Views.CuponesView.ErrorMustLoginWindow>();
            errorWnd.OnNavigatedTo("ErrorFormularioLibre");
            errorWnd.Owner = Application.Current.MainWindow;
            errorWnd.MouseDown += errorWnd_MouseDown;
            errorWnd.ShowDialog();
        }

        private bool IsCedulaIdentidadValida()
        {
            if (String.IsNullOrEmpty(this.TextBoxNewCedulaIdentidad.Text)) return false;
            if (this.TextBoxNewCedulaIdentidad.Text.Length < 6) return false;
            return true;
        }

        private bool IsEmailValido()
        {
            if (String.IsNullOrEmpty(this.TextBoxNewEmail.Text)) return false;

            // source: http://thedailywtf.com/Articles/Validating_Email_Addresses.aspx
            Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(this.TextBoxNewEmail.Text);
        }

        private bool IsNombreApellidoValido()
        {
            if (String.IsNullOrEmpty(this.TextBoxNewNombreApellido.Text)) return false;
            return true;
        }

        private void errorWnd_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window wnd = sender as Window;
            wnd.Close();
            this.TextBoxAlpha_GotFocus(this.TextBoxNewNombreApellido, null);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if ((navigationContext.Uri.OriginalString != "CuponesAutorizadoView") &
                (navigationContext.Uri.OriginalString != "CuponesLoginView"))
            {
                navigationContext.NavigationService.Region.Context = null;
                return;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.navigationService = navigationContext.NavigationService;
            if (this.DataContext == null)
            {
                this.DataContext = navigationContext.NavigationService.Region.Context;
            }
            this.TextBoxNewCedulaIdentidad.Clear();
            this.TextBoxNewEmail.Clear();
            this.TextBoxNewNombreApellido.Clear();
            this.RadioButtonCedulaFirstLetter.IsChecked = true;

            this.TextBoxAlpha_GotFocus(this.TextBoxNewNombreApellido, null);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }
    }
}
