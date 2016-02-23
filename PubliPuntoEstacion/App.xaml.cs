using Decktra.PubliPuntoEstacion.SyncAgentModule;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
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

            if (Decktra.PubliPuntoEstacion.Properties.Settings.Default.WebSyncOn)
            {
                SyncAgent syncAgentObj = bootStrapper.Container.Resolve<SyncAgent>();
                syncAgentObj.StartService();
            }

            bootStrapper.Container.RegisterType<MailService>(new InjectionConstructor(
                PubliPuntoEstacion.Properties.Settings.Default.KioskoID,
                PubliPuntoEstacion.Properties.Settings.Default.MailPwd));

            bootStrapper.Container.RegisterType<Services.GotoHomeTimerService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(PubliPuntoEstacion.Properties.Settings.Default.TimerInactividad));
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
