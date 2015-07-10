using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
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

        private void ButtonReclamarCuponPorPin_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.TextBoxCedulaIdentidad.Text))
            {
                string cedula = this.TextBoxCedulaIdentidad.Text;
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
                    });
                return;
            }

            var errorWnd = this.Container.Resolve<Views.CuponesView.ErrorMustLoginWindow>();
            errorWnd.OnNavigatedTo("ErrorLogin");
            errorWnd.Owner = Application.Current.MainWindow;
            errorWnd.MouseDown += errorWnd_MouseDown;
            errorWnd.ShowDialog();
        }

        private void errorWnd_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window wnd = sender as Window;
            wnd.Close();
            this.TextBoxNumeric_GotFocus(this.TextBoxCedulaIdentidad, null);
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
            this.TextBoxCedulaIdentidad.Clear();
            this.RadioButtonCedulaFirstLetter.IsChecked = true;

            this.TextBoxNumeric_GotFocus(this.TextBoxCedulaIdentidad, null);
        }

        void CuponesLoginView_OnPromocionAprobada(object sender, bool e)
        {
            if (!e)
            {
                //no aceptado
                var errorWnd = this.Container.Resolve<Views.CuponesView.ErrorMustLoginWindow>();
                errorWnd.OnNavigatedTo("ErrorPromocion");
                errorWnd.Owner = Application.Current.MainWindow;
                errorWnd.MouseDown += errorWnd_MouseDown;
                errorWnd.ShowDialog();
                if (navigationService.Journal.CanGoBack)
                {
                    navigationService.Journal.GoBack();
                }
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
            //Usuario no encontrado
            if (!e)
            {
                var errorWnd = this.Container.Resolve<Views.CuponesView.ErrorMustLoginWindow>();
                errorWnd.OnNavigatedTo("ErrorLogin");
                errorWnd.Owner = Application.Current.MainWindow;
                errorWnd.MouseDown += errorWnd_MouseDown;
                errorWnd.ShowDialog();
                this.TextBoxCedulaIdentidad.Clear();
                this.RadioButtonCedulaFirstLetter.IsChecked = true;
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
    }
}
