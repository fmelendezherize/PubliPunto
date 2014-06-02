using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.MainControlsModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;

namespace Decktra.PubliPuntoEstacion.MainControlsModule
{
    public class MainControlsModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public MainControlsModule(IRegionManager regionManager, IUnityContainer container)
        {
            this._regionManager = regionManager;
            this._container = container;
        }

        public void Initialize()
        {
            this._container.RegisterType<Object, HomeControlsView>("HomeControlsView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, HomeControlsView>("NuestrosClientesView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, HomeControlsView>("OfertasView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, BusquedaCategoriaView>("BusquedaCategoriaView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, BusquedaTecladoView>("BusquedaTecladoView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, DatosClienteView>("DatosClienteView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, ContactanosView>("ContactanosView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, ContactanosClientesView>("ContactanosClientesView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.CuponesInicioView>("CuponesInicioView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.CuponesLoginView>("CuponesLoginView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.CuponesAutorizadoView>("CuponesAutorizadoView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.ErrorMustLoginWindow>("ErrorMustLoginWindow",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.CuponSuccessWindow>("CuponSuccessWindow",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.CuponesCondicionesView>("CuponesCondicionesView",
                new ContainerControlledLifetimeManager());

            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("HomeControlsView", UriKind.Relative));
            this._regionManager.AddToRegion(RegionNames.REGION_NUESTROSCLIENTES_AREA, this._container.Resolve<NuestrosClientesView>());
            this._regionManager.AddToRegion(RegionNames.REGION_OFERTAS_AREA, this._container.Resolve<OfertasView>());
        }
    }
}
