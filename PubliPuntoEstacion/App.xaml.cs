using Decktra.PubliPuntoEstacion.SyncAgentModule;
using Microsoft.Practices.Unity;
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
            Bootstrapper bootStrapper = new Bootstrapper();
            bootStrapper.Run();

            if (Decktra.PubliPuntoEstacion.Properties.Settings.Default.WebSyncOn)
            {
                SyncAgent syncAgentObj = bootStrapper.Container.Resolve<SyncAgent>();
                syncAgentObj.StartService();
            }
        }
    }
}
