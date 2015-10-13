using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponSuccessWindow.xaml
    /// </summary>
    public partial class CuponSuccessWindow : Window
    {
        public CuponSuccessWindow()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.Close();
            //GlobalCommands.GoToHomeCommand.Execute(null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(8000);
            }).ContinueWith((q) =>
            {
                this.Close();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
