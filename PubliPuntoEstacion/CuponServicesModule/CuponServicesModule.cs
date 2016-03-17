using Decktra.PubliPuntoEstacion.SyncAgentModule;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Decktra.PubliPuntoEstacion.CuponServicesModule
{
    public class CuponServicesModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public CuponServicesModule(IRegionManager regionManager, IUnityContainer container)
        {
            this._regionManager = regionManager;
            this._container = container;
        }

        public void Initialize()
        {
            if (Decktra.PubliPuntoEstacion.Properties.Settings.Default.WebSyncOn)
            {
                SyncAgent syncAgentObj = this._container.Resolve<SyncAgent>();
                syncAgentObj.StartService();
            }

            this._container.RegisterType<MailService>(new InjectionConstructor(
                PubliPuntoEstacion.Properties.Settings.Default.KioskoID,
                PubliPuntoEstacion.Properties.Settings.Default.MailPwd));

            this._container.RegisterType<Services.GotoHomeTimerService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(PubliPuntoEstacion.Properties.Settings.Default.TimerInactividad));
        }
    }
}
