using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows.Controls;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesAutorizadoView.xaml
    /// </summary>
    public partial class CuponesAutorizadoView : UserControl, INavigationAware
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private static System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private int Counter;

        public CuponesAutorizadoView()
        {
            InitializeComponent();
        }

        //private void TextBlockReclamarCupon_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    var errorWnd = this.Container.Resolve<Views.CuponesView.CuponSuccessWindow>();
        //    errorWnd.Owner = Application.Current.MainWindow;
        //    errorWnd.ShowDialog();
        //}

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.NavigationService.Journal.CurrentEntry.Uri.OriginalString != "CuponesLoginView")
            {
                return false;
            }

            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.NavigationService.Region.Context = null;
            dispatcherTimer.Stop();
            dispatcherTimer = null;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.DataContext = navigationContext.NavigationService.Region.Context;
            Counter = 60;
            string strTitulo = string.Format("Hola $NOMBRE_USUARIO, ya tu cupón esta disponible. A continuación lee toda la información que te damos acerca de este, anota el número y recuerda que esta pantalla se cerrara en {0} segundos.", Counter);
            this.TextBlockTitulo.Text = strTitulo;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = sender as System.Windows.Threading.DispatcherTimer;
            Counter -= 1;
            string strTitulo = string.Format("Hola $NOMBRE_USUARIO, ya tu cupón esta disponible. A continuación lee toda la información que te damos acerca de este, anota el número y recuerda que esta pantalla se cerrara en {0} segundos.", Counter);
            this.TextBlockTitulo.Text = strTitulo;
            if (Counter == 0)
            {
                GlobalCommands.GoToHomeCommand.Execute(null);
            }
        }
    }
}
