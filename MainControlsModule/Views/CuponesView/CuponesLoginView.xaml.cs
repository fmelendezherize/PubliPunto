using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesLoginView.xaml
    /// </summary>
    public partial class CuponesLoginView : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        private IRegionNavigationService navigationService;

        private Control PreviousTextBox;

        public CuponesLoginView()
        {
            InitializeComponent();
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DatosClienteViewModel viewModel = (DatosClienteViewModel)this.DataContext;

            if (viewModel.PromocionCupon != null)
            {
                //bien
            }
            else
            {
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if ((navigationContext.NavigationService.Journal.CurrentEntry.Uri.OriginalString != "CuponesInicioView") |
                (navigationContext.NavigationService.Journal.CurrentEntry.Uri.OriginalString != "CuponesCondicionesView"))
            {
                return true;
            }

            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if ((navigationContext.Uri.OriginalString != "CuponesAutorizadoView") &
                (navigationContext.Uri.OriginalString != "CuponesLoginView") &
                (navigationContext.Uri.OriginalString != "CuponesInicioView") &
                (navigationContext.Uri.OriginalString != "CuponesCondicionesView") &
                (navigationContext.Uri.OriginalString != "CuponesRegistroView"))
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
                DatosClienteViewModel viewModel = (DatosClienteViewModel)this.DataContext;
                viewModel.PropertyChanged += OnPropertyChanged;
                viewModel.OnUsuarioAprobado += CuponesLoginView_OnUsuarioAprobado;
                viewModel.OnPromocionAprobada += CuponesLoginView_OnPromocionAprobada;
            }
            this.InitControls();
        }

        private void ButtonReclamarCupon_Click(object sender, RoutedEventArgs e)
        {
            if (IsDatosUsuarioValidos())
            {
                string cedula = this.TextBoxCedulaIdentidad.Text;
                if (this.RadioButtonCedulaFirstLetter.IsChecked.Value)
                {
                    cedula = "V" + this.TextBoxCedulaIdentidad.Text;
                }
                else
                {
                    cedula = "E" + this.TextBoxCedulaIdentidad.Text;
                };

                var wndConfirmacion = new CuponConfirmacionWindow();
                wndConfirmacion.Owner = Application.Current.MainWindow;
                wndConfirmacion.TextNombreUsuario.Text = this.TextBoxNombreApellido.Text;
                wndConfirmacion.TextBoxCedula.Text = cedula;
                if (!string.IsNullOrEmpty(this.TextBoxCodigoMovil.Text))
                {
                    wndConfirmacion.TextBoxMovil.Text = String.Format("{0}.{1}.{2}.{3}",
                        this.TextBoxCodigoMovil.Text,
                        this.TextBoxNumeroMovil.Text.Substring(0, 3),
                        this.TextBoxNumeroMovil.Text.Substring(3, 2),
                        this.TextBoxNumeroMovil.Text.Substring(5, 2));
                }

                if (wndConfirmacion.ShowDialog() == true)
                {
                    Usuario newUsuario = new Usuario
                    {
                        Cedula = cedula,
                        Nombre = this.TextBoxNombreApellido.Text,
                        Movil = this.TextBoxCodigoMovil.Text + this.TextBoxNumeroMovil.Text
                    };

                    ((DatosClienteViewModel)this.DataContext).ReclamarCuponCommand.Execute(newUsuario);
                }
                return;
            }

            var errorWnd = this.Container.Resolve<Views.DialogWindow>();
            errorWnd.OnNavigatedTo("ErrorLogin");
            errorWnd.Owner = Application.Current.MainWindow;
            errorWnd.Show();
        }

        private bool IsDatosUsuarioValidos()
        {
            const string patronMovil = "(0412|0424|0416|0414)";

            if (this.TextBoxCedulaIdentidad.Text.Length < 7) return false;
            if (this.TextBoxNombreApellido.Text.Length < 7) return false;

            if (!String.IsNullOrEmpty(this.TextBoxCodigoMovil.Text) &&
                (!Regex.IsMatch(this.TextBoxCodigoMovil.Text, patronMovil) || (this.TextBoxNumeroMovil.Text.Length != 7))) return false;
            if (!String.IsNullOrEmpty(this.TextBoxNumeroMovil.Text) &&
                (!Regex.IsMatch(this.TextBoxCodigoMovil.Text, patronMovil) || (this.TextBoxNumeroMovil.Text.Length != 7))) return false;

            return true;
        }

        private void InitControls()
        {
            this.TextBoxCedulaIdentidad.Clear();
            this.RadioButtonCedulaFirstLetter.IsChecked = true;
            this.TextBoxCodigoMovil.Clear();
            this.TextBoxNombreApellido.Clear();
            this.TextBoxNumeroMovil.Clear();
            this.TextBoxNumeroMovil_GotFocus(this.TextBoxCedulaIdentidad, null);
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

        private void TextBoxNumeroMovil_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PreviousTextBox != null)
            {
                this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DEDEE6"));
            }
            this.PreviousTextBox = sender as Control;
            this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#b1b1b8"));
            this.touchKeyboard.SetControlToWriteNumeric(this.PreviousTextBox);
        }

        void CuponesLoginView_OnPromocionAprobada(object sender, bool e)
        {
            if (!e)
            {
                //no aceptado
                var errorWnd = this.Container.Resolve<Views.DialogWindow>();
                errorWnd.OnNavigatedTo("ErrorPromocion");
                errorWnd.Owner = Application.Current.MainWindow;
                errorWnd.Show();
                return;
            }
            else
            {
                var processWnd = this.Container.Resolve<Views.CuponesView.CuponSuccessWindow>();
                processWnd.Owner = Application.Current.MainWindow;
                processWnd.ShowDialog();

                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("CuponesAutorizadoView", UriKind.Relative));
            }
        }

        void CuponesLoginView_OnUsuarioAprobado(object sender, bool e)
        {
            if (!e)
            {
                var errorWnd = this.Container.Resolve<Views.DialogWindow>();
                errorWnd.OnNavigatedTo("ErrorLogin");
                errorWnd.Owner = Application.Current.MainWindow;
                errorWnd.Show();
                return;
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private void ButtonRegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesRegistroView", UriKind.Relative));
        }

        private void ButtonVerCondiciones_Click(object sender, RoutedEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesCondicionesView", UriKind.Relative));
        }

        private void TextBoxNewNombreApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            var caret = this.TextBoxNombreApellido.CaretIndex;
            this.TextBoxNombreApellido.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.TextBoxNombreApellido.Text.ToLower());
            this.TextBoxNombreApellido.CaretIndex = caret;
        }

        private void TextBoxCodigoMovil_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxCodigoMovil.Text.Length == 4) { this.TextBoxNumeroMovil.Focus(); }
        }

        private void TextBoxNumeroMovil_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TextBoxNumeroMovil.Text.Length == 0)
            {
                this.TextBoxCodigoMovil.Focus();
                this.TextBoxCodigoMovil.CaretIndex = 4;
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
    }
}
