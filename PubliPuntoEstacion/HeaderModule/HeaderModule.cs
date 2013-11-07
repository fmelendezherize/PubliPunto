using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decktra.PubliPuntoEstacion.HeaderModule.Views;
using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

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
        }
    }
}
