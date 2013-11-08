using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

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
            this._container.RegisterType<Object, BusquedaCategoriaView>("BusquedaCategoriaView");
            this._container.RegisterType<Object, BusquedaTecladoView>("BusquedaTecladoView");

            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("HomeControlsView", UriKind.Relative));
        }
    }
}
