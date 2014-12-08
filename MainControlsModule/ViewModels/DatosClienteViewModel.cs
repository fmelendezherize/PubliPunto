using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
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

        public event EventHandler<bool> OnUsuarioAprobado;
        public event EventHandler<bool> OnPromocionAprobada;

        public DatosClienteViewModel()
        {
            this.ShowEnteComercialCommand = new DelegateCommand<string>(this.ShowEnteComercial);
            this.ReclamarCuponCommand = new DelegateCommand<Usuario>(this.ReclamarCupon);
        }

        private void ReclamarCupon(Usuario usuarioKiosko)
        {
            if (usuarioKiosko == null) return;

            //Validar Usuario
            using (var usuarioRepository = new UsuariosRepository())
            {
                UsuarioValidado = usuarioRepository.RetrieveUsuarioBy(usuarioKiosko.Cedula, usuarioKiosko.Pin);

                if (UsuarioValidado == null)
                {
                    if (!string.IsNullOrEmpty(usuarioKiosko.Cedula) && !string.IsNullOrEmpty(usuarioKiosko.Email))
                    {
                        //chance de registro
                        usuarioRepository.Add(usuarioKiosko);
                    }
                    else
                    {
                        if (OnUsuarioAprobado != null) { OnUsuarioAprobado(this, false); }
                        return;
                    }
                }
            };

            ///Promocion
            PromocionCupon = null;
            PromocionCupon = new EnteComercialRepository().UpdatePromocionCupon(PromocionSelected, UsuarioValidado);
            if (PromocionCupon == null)
            {
                if (OnPromocionAprobada != null) { OnPromocionAprobada(this, false); }
            }
            else
            {
                this.RaisePropertyChanged(() => this.PromocionCupon);
                this.RaisePropertyChanged(() => this.UsuarioValidado);
                if (OnPromocionAprobada != null) { OnPromocionAprobada(this, true); }
            }
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

        public void SelectPromocion(Promocion promocion)
        {
            this.PromocionSelected = promocion;
            this.RaisePropertyChanged(() => this.PromocionSelected);
        }
    }
}