using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for ErrorMustLoginWindow.xaml
    /// </summary>
    public partial class ErrorMustLoginWindow : Window
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public ErrorMustLoginWindow()
        {
            InitializeComponent();
        }

        private void ImageFacebook_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }

        public void OnNavigatedTo(string accion)
        {
            if (accion == "MustLogin")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Antes de poder reclamar su cupón debe iniciar sesión en Facebook.";
            }
            else if (accion == "ErrorLogin")
            {
                this.TextBlockTitulo.Text = "Error !";
                this.TextBlockMensaje.Text = "Su correo de facebook o contraseña son incorrectos. Por favor vuelva a introducirlos.";
            }
        }
    }
}
