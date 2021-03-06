﻿using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.KioskoServicesModule;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System.Globalization;
using System.Text.RegularExpressions;
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

        private string NombreEnteComercial;

        [Dependency]
        public GotoHomeTimerService TimerService { get; set; }

        Views.DialogWindow errorWnd;

        public ContactanosClientesView()
        {
            InitializeComponent();
            this.touchKeyboard.OnButtonClick += TouchKeyboard_OnButtonClick;
        }

        private void TouchKeyboard_OnButtonClick(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TimerService.Start();
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

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (errorWnd != null) { errorWnd.Close(); }
            TimerService.Stop();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.navigationService = navigationContext.NavigationService;
            NombreEnteComercial = navigationContext.Parameters["NombreEnteComercial"].ToString();

            this.TextBoxEmail.Clear();
            this.TextBoxNombre.Clear();
            this.TextBoxTelefono.Clear();
            this.TextBoxComentario.Clear();
            this.TextBoxTelefonoPrefix.Clear();

            this.FormularioPanel.Visibility = System.Windows.Visibility.Visible;
            this.FormularioEnviadoPanel.Visibility = System.Windows.Visibility.Collapsed;
            this.touchKeyboard.Visibility = System.Windows.Visibility.Visible;

            this.TextBoxNombre_GotFocus(this.TextBoxNombre, null);

            TimerService.Start();
        }

        private bool IsDatosContactanosValidos()
        {
            if (string.IsNullOrEmpty(this.TextBoxNombre.Text)) return false;
            if (string.IsNullOrEmpty(this.TextBoxTelefono.Text)) return false;
            if (string.IsNullOrEmpty(this.TextBoxTelefonoPrefix.Text)) return false;
            if (string.IsNullOrEmpty(this.TextBoxComentario.Text)) return false;

            string mailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!Regex.IsMatch(this.TextBoxEmail.Text, mailPattern)) return false;

            return true;
        }

        private void ButtonEnviar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsDatosContactanosValidos())
            {
                using (var repository = new DatosContactosRepository())
                {
                    DatosContacto newContacto = new DatosContacto()
                    {
                        Comentario = this.TextBoxComentario.Text,
                        Email = this.TextBoxEmail.Text,
                        Destinatario = NombreEnteComercial,
                        Nombre = this.TextBoxNombre.Text,
                        Telefono = this.TextBoxTelefonoPrefix.Text + this.TextBoxTelefono.Text
                    };
                    repository.Add(newContacto);
                    Container.Resolve<MailService>().SendMailFromContactanos(
                        newContacto.Email, newContacto.Destinatario, newContacto.Nombre, newContacto.Telefono, newContacto.Comentario);
                }

                this.FormularioPanel.Visibility = System.Windows.Visibility.Collapsed;
                this.FormularioEnviadoPanel.Visibility = System.Windows.Visibility.Visible;
                this.touchKeyboard.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                errorWnd = this.Container.Resolve<Views.DialogWindow>();
                errorWnd.ShowErrorFormulario();
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

        private void TextBoxTelefonoPrefix_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxTelefonoPrefix.Text.Length == 4) { this.TextBoxTelefono.Focus(); }
        }
    }
}
