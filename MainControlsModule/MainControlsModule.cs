using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decktra.PubliPuntoEstacion.Interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace Decktra.PubliPuntoEstacion.MainControlsModule
{
    public class MainControlsModule: IModule
    {
        private readonly IRegionManager regionManager;

        public MainControlsModule(IRegionManager regionManager) 
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            
            regionManager.RegisterViewWithRegion(RegionNames.REGION_WORK_AREA, typeof(BusquedaCategoriaView));
        }
    }
}
