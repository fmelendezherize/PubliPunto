using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.KioskoServicesModule;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesCondiciones.xaml
    /// </summary>
    public partial class CuponesCondicionesView : UserControl, INavigationAware
    {
        private IRegionNavigationService navigationService;

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public GotoHomeTimerService TimerService { get; set; }

        public CuponesCondicionesView()
        {
            InitializeComponent();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            TimerService.Stop();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.navigationService = navigationContext.NavigationService;
            if (this.DataContext == null)
            {
                this.DataContext = navigationContext.NavigationService.Region.Context;
            }
            TimerService.Start();
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private void TextBlockReclamarCupon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }

        private Point LastPosition;
        private bool TouchMoved;
        private const short DIFF_POSITION_CONST = 5;

        private void ScrollViewer_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if (!TouchMoved)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ScrollViewer_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            LastPosition = e.GetTouchPoint(this.TextBlockTitulo).Position;
        }

        private void ScrollViewer_TouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            MoverScrollByTouch(e);
        }

        private void MoverScrollByTouch(TouchEventArgs e)
        {
            var actualPosition = e.GetTouchPoint(this.TextBlockTitulo).Position;

            var diffPosition = actualPosition.Y - LastPosition.Y;
            if ((diffPosition > (DIFF_POSITION_CONST * -1)) & (diffPosition < DIFF_POSITION_CONST))
            {
                TouchMoved = false;
                return;
            };

            double offset = this.ScrollViewerTexto.VerticalOffset - (diffPosition / DIFF_POSITION_CONST);
            if (actualPosition.Y > LastPosition.Y)
            {
                this.ScrollViewerTexto.ScrollToVerticalOffset(offset);
                TouchMoved = true;
                e.Handled = true;
            }
            else
            {
                this.ScrollViewerTexto.ScrollToVerticalOffset(offset);
                TouchMoved = true;
                e.Handled = true;
            }
        }
    }
}
