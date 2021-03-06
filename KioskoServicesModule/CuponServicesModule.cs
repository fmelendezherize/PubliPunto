﻿using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Decktra.PubliPuntoEstacion.KioskoServicesModule
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
            this._container.RegisterType<GotoHomeTimerService>(new ContainerControlledLifetimeManager());
            this._container.RegisterType<MailService>(new ContainerControlledLifetimeManager());
            this._container.RegisterType<SyncAgent>(new ContainerControlledLifetimeManager());
            this._container.RegisterType<SmsMessageService>(new ContainerControlledLifetimeManager());
        }
    }
}
