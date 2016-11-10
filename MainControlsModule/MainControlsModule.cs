using Decktra.PubliPuntoEstacion.MainControlsModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
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
            this._container.RegisterType<Object, Views.CuponesView.CuponesRegistroView>("CuponesRegistroView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.CuponesAutorizadoView>("CuponesAutorizadoView",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.DialogWindow>("ErrorMustLoginWindow",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.DialogCuponSuccessWindow>("CuponSuccessWindow",
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, Views.CuponesView.CuponesCondicionesView>("CuponesCondicionesView",
                new ContainerControlledLifetimeManager());
        }
    }
}
