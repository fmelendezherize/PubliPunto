using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class DatosClienteViewModel : NotificationObject
    {
        public ICommand ShowEnteComercialCommand { get; set; }
        public ICommand ReclamarCuponCommand { get; set; }
        public EnteComercial EnteComercial { get; set; }
        public Promocion PromocionSelected { get; set; }

        public PromocionCupon PromocionCupon { get; set; }
        public Usuario UsuarioValidado { get; set; }

        public DatosClienteViewModel()
        {
            this.ShowEnteComercialCommand = new DelegateCommand<string>(this.ShowEnteComercial);
            this.ReclamarCuponCommand = new DelegateCommand<Usuario>(this.ReclamarCupon);
        }

        private void ReclamarCupon(Usuario usuarioKiosko)
        {
            if (usuarioKiosko == null) return;

            //Validar Usuario
            PromocionCupon = null;

            using (var usuarioRepository = new UsuariosRepository())
            {
                UsuarioValidado = usuarioRepository.RetrieveUsuarioBy(usuarioKiosko.Cedula, usuarioKiosko.Pin);
            };

            if (UsuarioValidado == null) this.RaisePropertyChanged<Usuario>(() => UsuarioValidado);


            //PromocionCupon = new PromocionCupon();
            this.RaisePropertyChanged<PromocionCupon>(() => PromocionCupon);
        }

        private void ShowEnteComercial(string obj)
        {
            using (var _enteComercialRepository = new EnteComercialRepository())
            {
                EnteComercial = _enteComercialRepository.FindBy(int.Parse(obj));
                if (EnteComercial == null) GlobalCommands.GoToHomeCommand.Execute(null);

                this.RaisePropertyChanged(() => this.EnteComercial);
            }
        }
    }
}