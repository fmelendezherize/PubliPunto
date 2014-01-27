using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class NuestrosClientesViewModel : NotificationObject
    {
        public EnteComercial EnteComercial0 { get; set; }

        public NuestrosClientesViewModel()
        {
            this.EnteComercial0 = new EnteComercial { Id = 0, Nombre = "SIESCOM C.A." };
        }
    }
}
