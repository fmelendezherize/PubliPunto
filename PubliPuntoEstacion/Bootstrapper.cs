using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;

namespace Decktra.PubliPuntoEstacion
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            moduleCatalog.AddModule(typeof(KioskoServicesModule.CuponServicesModule));
            moduleCatalog.AddModule(typeof(HeaderModule.HeaderModule));
            moduleCatalog.AddModule(typeof(MainControlsModule.MainControlsModule));
            moduleCatalog.AddModule(typeof(FooterModule.FooterModule));
        }

        protected override Microsoft.Practices.Prism.Logging.ILoggerFacade CreateLogger()
        {
            return new Logger();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }
    }
}
