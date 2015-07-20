using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class DatosClienteViewModel : BindableBase
    {
        public ICommand ShowEnteComercialCommand { get; set; }
        public ICommand ReclamarCuponCommand { get; set; }

        public PromocionCupon PromocionCupon { get; set; }
        public Usuario UsuarioValidado { get; set; }

        private EnteComercial _enteComercial;
        public EnteComercial EnteComercial
        {
            get { return _enteComercial; }
            set { SetProperty(ref _enteComercial, value); }
        }

        private List<Promocion> _promocionesActivas;
        public List<Promocion> PromocionesActivas
        {
            get { return _promocionesActivas; }
            set { SetProperty(ref _promocionesActivas, value); }
        }

        private Promocion _promocionSelected;
        public Promocion PromocionSelected
        {
            get { return _promocionSelected; }
            set { SetProperty(ref this._promocionSelected, value); }
        }

        public event EventHandler<bool> OnUsuarioAprobado;
        public event EventHandler<bool> OnPromocionAprobada;

        public DatosClienteViewModel()
        {
            this.ShowEnteComercialCommand = new DelegateCommand<string>(this.ShowEnteComercial);
            this.ReclamarCuponCommand = new DelegateCommand<Usuario>(this.ReclamarCupon);
        }

        private void ShowEnteComercial(string obj)
        {
            using (var enteComercialRepository = new EnteComercialRepository())
            {
                EnteComercial = enteComercialRepository.FindBy(int.Parse(obj));
                if (EnteComercial == null)
                {
                    GlobalCommands.GoToHomeCommand.Execute(null);
                    return;
                }
                PromocionesActivas = new List<Promocion>(enteComercialRepository.GetPromocionesActivasBy(EnteComercial.Id));
            }
        }

        private void ReclamarCupon(Usuario usuarioKiosko)
        {
            if (usuarioKiosko == null) return;

            //Validar Usuario
            //using (var usuarioRepository = new UsuariosRepository())
            //{
            //    UsuarioValidado = usuarioRepository.RetrieveUsuarioBy(usuarioKiosko.Cedula, usuarioKiosko.Pin);

            //    if (UsuarioValidado == null)
            //    {
            //        if (!string.IsNullOrEmpty(usuarioKiosko.Cedula) && !string.IsNullOrEmpty(usuarioKiosko.Email)
            //            && !string.IsNullOrEmpty(usuarioKiosko.Nombre))
            //        {
            //            //chance de registro
            //            usuarioRepository.Add(usuarioKiosko);
            //            UsuarioValidado = usuarioKiosko;
            //        }
            //        else
            //        {
            //            if (OnUsuarioAprobado != null) { OnUsuarioAprobado(this, false); }
            //            return;
            //        }
            //    }
            //};

            ///Promocion
            //PromocionCupon = null;
            //PromocionCupon = new EnteComercialRepository().UpdatePromocionCupon(PromocionSelected, UsuarioValidado);
            //if (PromocionCupon == null)
            //{
            //    if (OnPromocionAprobada != null) { OnPromocionAprobada(this, false); }
            //}
            //else
            //{
            //    this.RaisePropertyChanged(() => this.PromocionCupon);
            //    this.RaisePropertyChanged(() => this.UsuarioValidado);
            //    if (OnPromocionAprobada != null) { OnPromocionAprobada(this, true); }
            //}
            if (OnPromocionAprobada != null) { OnPromocionAprobada(this, true); }
        }
    }
}