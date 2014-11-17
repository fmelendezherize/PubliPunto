using Decktra.PubliPuntoEstacion.HeaderModule.Views;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Timers;

namespace Decktra.PubliPuntoEstacion.HeaderModule
{
    class HeaderModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        private Timer HomeTimer;

        public HeaderModule(IRegionManager regionManager, IUnityContainer container)
        {
            this._regionManager = regionManager;
            this._container = container;

            HomeTimer = new Timer(5000);
            HomeTimer.AutoReset = false;
            HomeTimer.Elapsed += HomeTimer_Elapsed;
        }

        public void Initialize()
        {
            this._container.RegisterType<Object, HeaderView>("HeaderView");
            this._regionManager.RequestNavigate(RegionNames.REGION_HEADER_AREA, new Uri("HeaderView", UriKind.Relative));

            GlobalCommands.GoToHomeCommand = new DelegateCommand<Object>(this.GoToHome);
            GlobalCommands.GoToBusquedaCategoriaCommand = new DelegateCommand<Object>(this.GoToBusquedaCategoria);
            GlobalCommands.GoToBusquedaTecladoCommand = new DelegateCommand<Object>(this.GoToBusquedaTeclado);
        }

        private void GoToBusquedaTeclado(object obj)
        {
            this.HomeTimer.Stop();
            this.HomeTimer.Start();

            NavigationParameters query = new NavigationParameters();
            query.Add("reset", "true");
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("BusquedaTecladoView" + query.ToString(), UriKind.Relative));
        }

        private void GoToBusquedaCategoria(object obj)
        {
            this.HomeTimer.Stop();
            this.HomeTimer.Start();

            NavigationParameters query = new NavigationParameters();
            query.Add("reset", "true");
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA,
                new Uri("BusquedaCategoriaView" + query.ToString(), UriKind.Relative));
        }

        private void GoToHome(object obj)
        {
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("HomeControlsView", UriKind.Relative));
        }

        void HomeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //MessageBox.Show("Hola");
        }
    }
}
