using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.Views.CuponesView
{
    /// <summary>
    /// Interaction logic for CuponesInicioView.xaml
    /// </summary>
    public partial class CuponesInicioView : UserControl, INavigationAware
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        private IRegionNavigationService navigationService;

        [Dependency]
        public Services.GotoHomeTimerService TimerService { get; set; }

        Views.DialogWindow errorWnd;

        public CuponesInicioView()
        {
            InitializeComponent();
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
            if (this.DataContext == null)
            {
                this.DataContext = navigationContext.NavigationService.Region.Context;
            }
            this.navigationService = navigationContext.NavigationService;
            TimerService.Start();
        }

        private void ImageFacebook_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }

        //private void TextBlockReclamarCupon_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    Promocion promocion = ((TextBlock)sender).DataContext as Promocion;
        //    ((DatosClienteViewModel)this.DataContext).PromocionSelected = promocion;

        //    if (!promocion.HasCuponesDisponibles)
        //    {
        //        //no aceptado
        //        var errorWnd = this.Container.Resolve<Views.DialogWindow>();
        //        errorWnd.ShowErrorPromocion(string.Empty);
        //    }
        //    else
        //    {
        //        this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
        //            new Uri("CuponesLoginView", UriKind.Relative));
        //    }
        //}

        private void TextBlockVerCondiciones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Promocion promocion = ((TextBlock)sender).DataContext as Promocion;
            ((DatosClienteViewModel)this.DataContext).PromocionSelected = promocion;

            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesCondicionesView", UriKind.Relative));
        }

        private void ButtonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private void ButtonReclamarCupon_Click(object sender, RoutedEventArgs e)
        {
            Promocion promocion = ((Button)sender).DataContext as Promocion;
            ((DatosClienteViewModel)this.DataContext).PromocionSelected = promocion;

            if (!promocion.HasCuponesDisponibles)
            {
                //no aceptado
                errorWnd = this.Container.Resolve<Views.DialogWindow>();
                errorWnd.ShowErrorPromocion(string.Empty);
            }
            else
            {
                this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                    new Uri("CuponesLoginView", UriKind.Relative));
            }
        }
    }
}
