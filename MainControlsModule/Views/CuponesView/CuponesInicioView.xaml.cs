﻿using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
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

        public CuponesInicioView()
        {
            InitializeComponent();
        }

        private void ImageFacebook_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }

        private void TextBlockReclamarCupon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((DatosClienteViewModel)this.DataContext).PromocionSelected = (Promocion)((TextBlock)sender).DataContext;

            this.RegionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("CuponesLoginView", UriKind.Relative));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //if ((navigationContext.Uri.OriginalString != "CuponesLoginView") &
            //    (navigationContext.Uri.OriginalString != "CuponesCondicionesView"))
            //{
            //    navigationContext.NavigationService.Region.Context = null;
            //    return;
            //}
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.DataContext = null;
            this.DataContext = navigationContext.NavigationService.Region.Context;
            this.navigationService = navigationContext.NavigationService;
        }

        private void TextBlockVerCondiciones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((DatosClienteViewModel)this.DataContext).PromocionSelected = (Promocion)((TextBlock)sender).DataContext;

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
    }
}
