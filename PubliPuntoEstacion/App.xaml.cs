using Decktra.PubliPuntoEstacion.Interfaces;
using Decktra.PubliPuntoEstacion.KioskoServicesModule;
using Decktra.PubliPuntoEstacion.MainControlsModule.Views;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Windows;

namespace Decktra.PubliPuntoEstacion
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Enterprise Library Configuration
            IConfigurationSource config = ConfigurationSourceFactory.Create();
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.SetLogWriter(new LogWriterFactory(config).Create());
            ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
            ExceptionManager exManager = factory.CreateManager();
            ExceptionPolicy.SetExceptionManager(exManager);
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;

            //Bootstrapper
            Bootstrapper bootStrapper = new Bootstrapper();
            bootStrapper.Run();

            IUnityContainer container = bootStrapper.Container;
            IRegionManager regionManager = container.Resolve<IRegionManager>();

            //Start Services
            container.Resolve<GotoHomeTimerService>(new ParameterOverride(
                "timerInactividad", PubliPuntoEstacion.Properties.Settings.Default.TimerInactividad));
            container.Resolve<MailService>(
                new ParameterOverrides
                {
                    {"kioskoID", PubliPuntoEstacion.Properties.Settings.Default.KioskoID},
                    {"pwdMail", PubliPuntoEstacion.Properties.Settings.Default.MailPwd}
                });
            if (Decktra.PubliPuntoEstacion.Properties.Settings.Default.WebSyncOn)
            {
                container.Resolve<SyncAgent>(
                    new ParameterOverrides
                    {
                        {"webSyncServerAddress", PubliPuntoEstacion.Properties.Settings.Default.WebSyncServerAddress},
                        {"videosPath", PubliPuntoEstacion.Properties.Settings.Default.VideosPath}
                    });
            }
            container.Resolve<SmsMessageService>(
                new ParameterOverrides
                {
                    {"envioSMS_ON", PubliPuntoEstacion.Properties.Settings.Default.EnvioSMS_ON},
                    {"SMS_UserName", PubliPuntoEstacion.Properties.Settings.Default.SMS_UserName},
                    {"SMS_Pwd", PubliPuntoEstacion.Properties.Settings.Default.SMS_Pwd},
                });


            //Start MainView
            regionManager.RequestNavigate(RegionNames.REGION_WORK_AREA, new Uri("HomeControlsView", UriKind.Relative));
            regionManager.AddToRegion(RegionNames.REGION_NUESTROSCLIENTES_AREA, container.Resolve<NuestrosClientesView>());
            regionManager.AddToRegion(RegionNames.REGION_OFERTAS_AREA, container.Resolve<OfertasView>());
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
#if (!DEBUG)
            try
            {
                ExceptionPolicy.HandleException(e.ExceptionObject as Exception, "Policy");
            }
            finally
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Environment.Exit(1);
            }
#endif
        }
    }
}
