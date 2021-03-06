﻿using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Unity;
using Prism.Regions;
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
        private int _counter;

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
            if ((navigationContext.NavigationService.Journal.CurrentEntry.Uri.OriginalString == "CuponesLoginView") |
                (navigationContext.NavigationService.Journal.CurrentEntry.Uri.OriginalString == "CuponesRegistroView"))
            {
                return true;
            }

            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.NavigationService.Region.Context = null;
            StopHomeTimer();
        }

        private static void StopHomeTimer()
        {
            dispatcherTimer.Stop();
            dispatcherTimer = null;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (this.DataContext == null) this.DataContext = navigationContext.NavigationService.Region.Context;
            StartHomeTimer();
        }

        private void StartHomeTimer()
        {
            _counter = 60;
            this.TextBlockTiempoRestante.Text = _counter.ToString();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = sender as System.Windows.Threading.DispatcherTimer;
            _counter -= 1;
            this.TextBlockTiempoRestante.Text = _counter.ToString();
            if (_counter == 0)
            {
                GlobalCommands.GoToHomeCommand.Execute(null);
            }
        }

        private void ButtonCerrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GlobalCommands.GoToHomeCommand.Execute(null);
        }
    }
}
