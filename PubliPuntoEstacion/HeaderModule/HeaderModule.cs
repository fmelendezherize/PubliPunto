using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Decktra.PubliPuntoEstacion.HeaderModule
{
    class HeaderModule: IModule
    {
        private readonly IRegionManager regionManager;

        public HeaderModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.REGION_HEADER_AREA, typeof(HeaderView));
        }
    }
}
