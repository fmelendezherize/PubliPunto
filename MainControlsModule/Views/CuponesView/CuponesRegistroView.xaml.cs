using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
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

        private Control PreviousTextBox;

        public CuponesRegistroView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PreviousTextBox != null)
            {
                this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DEDEE6"));
            }
            this.PreviousTextBox = sender as Control;
            this.PreviousTextBox.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#b1b1b8"));
            this.touchKeyboard.SetControlToWriteAlphaNumeric(this.PreviousTextBox);
        }

        private void ButtonRegistro_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesAutorizadoView", UriKind.Relative));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (navigationContext.Uri.OriginalString != "CuponesAutorizadoView")
            {
                navigationContext.NavigationService.Region.Context = null;
                return;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.DataContext = navigationContext.NavigationService.Region.Context;
            this.TextBoxApellido.Clear();
            this.TextBoxEmail.Clear();
            this.TextBoxNombre.Clear();
            this.PasswordBoxConfirmPassword.Clear();
            this.PasswordBoxPassword.Clear();
            this.TextBox_GotFocus(this.TextBoxNombre, null);
        }
    }
}
