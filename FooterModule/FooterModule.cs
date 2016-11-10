using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.Library;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace Decktra.PubliPuntoEstacion.FooterModule
{
    public class FooterModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public FooterModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.REGION_FOOTER_AREA, typeof(AreaPublicidad));
            GlobalCommands.GoToContactanosCommand = new DelegateCommand<Object>(GoToContactnos);
        }

        private void GoToContactnos(object obj)
        {
            this._regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("ContactanosView", UriKind.Relative));
        }
    }
}
