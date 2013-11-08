using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decktra.PubliPuntoEstacion.HeaderModule.Views;
using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Commands;

namespace Decktra.PubliPuntoEstacion.HeaderModule
{
    class HeaderModule: IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public HeaderModule(IRegionManager regionManager, IUnityContainer container)
        {
            this._regionManager = regionManager;
            this._container = container;
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
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("BusquedaTecladoView", UriKind.Relative));
        }

        private void GoToBusquedaCategoria(object obj)
        {
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("BusquedaCategoriaView", UriKind.Relative));
        }

        private void GoToHome(object obj)
        {
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("HomeControlsView", UriKind.Relative));
        }
    }
}
