using Decktra.PubliPuntoEstacion.Interfaces;
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

        private void ButtonReclamarCuponPorPin_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(this.TextBoxCedulaIdentidad.Text)) &
                (!String.IsNullOrEmpty(this.TextBoxPIN.Password)))
            {
                this.ReclamarCupon();
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
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
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
            if (navigationContext.NavigationService.Journal.CurrentEntry.Uri.OriginalString != "CuponesInicioView")
            {
                return false;
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if ((navigationContext.Uri.OriginalString != "CuponesAutorizadoView") &
                (navigationContext.Uri.OriginalString != "CuponesLoginView") &
                (navigationContext.Uri.OriginalString != "CuponesInicioView"))
            {
                navigationContext.NavigationService.Region.Context = null;
                return;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.navigationService = navigationContext.NavigationService;
            this.DataContext = navigationContext.NavigationService.Region.Context;

            this.TextBoxCedulaIdentidad.Clear();
            this.TextBoxPIN.Clear();

            this.TextBoxNewCedulaIdentidad.Clear();
            this.TextBoxNewEmail.Clear();
            this.TextBoxNewNombreApellido.Clear();

            this.TextBoxAlpha_GotFocus(this.TextBoxNewNombreApellido, null);
        }

        private void ButtonReclamarCuponPorFormulario_Click(object sender, RoutedEventArgs e)
        {
            if ((!String.IsNullOrEmpty(this.TextBoxNewCedulaIdentidad.Text)) &
                (!String.IsNullOrEmpty(this.TextBoxNewEmail.Text)) &
                (!String.IsNullOrEmpty(this.TextBoxNewNombreApellido.Text)))
            {
                this.ReclamarCupon();
                return;
            }

            var errorWnd = this.Container.Resolve<Views.CuponesView.ErrorMustLoginWindow>();
            errorWnd.OnNavigatedTo("ErrorFormularioLibre");
            errorWnd.Owner = Application.Current.MainWindow;
            errorWnd.MouseDown += errorWnd_MouseDown;
            errorWnd.ShowDialog();
        }

        private void ReclamarCupon()
        {
            var processWnd = this.Container.Resolve<Views.CuponesView.CuponSuccessWindow>();
            processWnd.Owner = Application.Current.MainWindow;
            processWnd.ShowDialog();

            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesAutorizadoView", UriKind.Relative));
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
