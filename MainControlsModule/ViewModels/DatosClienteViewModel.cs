using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.KioskoServicesModule;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Decktra.PubliPuntoEstacion.MainControlsModule.ViewModels
{
    public class DatosClienteViewModel : BindableBase
    {
        public ICommand ShowEnteComercialCommand { get; set; }
        public ICommand ReclamarCuponCommand { get; set; }

        private PromocionCupon _promocionCupon;
        public PromocionCupon PromocionCupon
        {
            get { return _promocionCupon; }
            set { SetProperty(ref _promocionCupon, value); }
        }

        private Usuario _usuarioValidado;
        public Usuario UsuarioValidado
        {
            get { return _usuarioValidado; }
            set { SetProperty(ref _usuarioValidado, value); }
        }

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

        private string _mensajeMovil;
        public string MensajeMovil
        {
            get { return _mensajeMovil; }
            set { SetProperty(ref this._mensajeMovil, value); }
        }

        public event EventHandler<object> OnPromocionAprobada;
        public event EventHandler<string> OnPromocionNoAprobada;

        [Dependency]
        public ILoggerFacade Logger { get; set; }

        [Dependency]
        public SmsMessageService SmsService { get; set; }

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
            using (var usuarioRepository = new UsuariosRepository())
            {
                usuarioRepository.AddOrUpdate(usuarioKiosko);
                UsuarioValidado = usuarioKiosko;
            };

            ///Promocion
            using (var repository = new EnteComercialRepository())
            {
                try
                {
                    PromocionCupon = null;
                    PromocionCupon = repository.ProcesarPromocionCupon(PromocionSelected, UsuarioValidado);
                    DoOnPromocionAprobada();
                }
                catch (InvalidOperationException ex)
                {
                    if (OnPromocionNoAprobada != null) { OnPromocionNoAprobada(this, ex.Message); }
                }
            }
        }

        private void DoOnPromocionAprobada()
        {
            if (PromocionCupon.SmsSent)
            {
                this.MensajeMovil = "Tu cupón ya fue enviado a tu móvil.";
                if (OnPromocionAprobada != null) { OnPromocionAprobada(this, true); }
                return;
            }


            SmsService.EnviarSmsToCliente(PromocionCupon, UsuarioValidado, PromocionSelected).
                ContinueWith((t) =>
                {
                    if (t.Result)
                    {
                        using (var repositoryPostSms = new EnteComercialRepository())
                        {
                            repositoryPostSms.UpdatePromocionCuponBySmsSent(PromocionCupon.Id);
                            this.MensajeMovil = "Tu cupón fue enviado a tu móvil con éxito.";
                        }
                    }
                    else
                    {
                        this.MensajeMovil = "Tu cupón no pudo ser enviado a tu móvil.";
                    }
                }, CancellationToken.None, TaskContinuationOptions.NotOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
            if (OnPromocionAprobada != null) { OnPromocionAprobada(this, true); }
        }
    }
}