using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.Library;
using Decktra.PubliPuntoEstacion.MainControlsModule.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;

namespace Decktra.PubliPuntoEstacion.MainControlsModule
{
    public class MainControlsModule: IModule
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
            this._container.RegisterType<Object, HomeControlsView>("HomeControlsView");
            this._container.RegisterType<Object, BusquedaCategoriaView>("BusquedaCategoriaView", 
                new ContainerControlledLifetimeManager());
            this._container.RegisterType<Object, BusquedaTecladoView>("BusquedaTecladoView");
            this._container.RegisterType<Object, DatosClienteView>("DatosClienteView");

            GlobalCommands.GoToDatosClienteCommand = new DelegateCommand<object>(this.GoToDatosCliente);

            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("HomeControlsView", UriKind.Relative));
        }

        private void GoToDatosCliente(object obj)
        {
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("DatosClienteView", UriKind.Relative));
        }
    }
}
