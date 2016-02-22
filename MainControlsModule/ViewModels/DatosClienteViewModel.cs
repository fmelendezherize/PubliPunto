using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Decktra.PubliPuntoEstacion.CoreApplication.Repository;
using Decktra.PubliPuntoEstacion.Library;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

            EnviarSmsToCliente(PromocionCupon.CodigoCanjeo, UsuarioValidado.Movil, PromocionSelected.Descripcion).
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

        private async Task<bool> EnviarSmsToCliente(string codigoCupon, string phoneNumber, string descripcionPromocion)
        {
            if ((string.IsNullOrEmpty(codigoCupon)) || (string.IsNullOrEmpty(phoneNumber))) return false;
            if (Properties.Settings.Default.EnvioSMS_ON == false) return false;

            using (var client = new HttpClient())
            {
                string smsUserName = Properties.Settings.Default.SMS_UserName;
                string smsPwd = Properties.Settings.Default.SMS_Pwd;
                string mensajeEnvio = String.Format("Cupón: {0}. ", codigoCupon) +
                    String.Format("Promo: {0}. ", descripcionPromocion) +
                    Properties.Settings.Default.SMS_MensajePie;

                string uriStr = string.Format("secure/insert.php?uname={0}&pass={1}&num={2}&msg={3}",
                    smsUserName, smsPwd, phoneNumber, Uri.EscapeUriString(mensajeEnvio));

                client.BaseAddress = new Uri("http://041x.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uriStr);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        Logger.Log(string.Format("Error enviando SMS: {0}", response.StatusCode.ToString()),
                            Category.Info, Priority.Low);
                        return false;
                    }
                }
                catch (HttpRequestException e)
                {
                    Logger.Log(string.Format("Error enviando SMS: {0}", e.InnerException.Message),
                        Category.Info, Priority.Low);
                    return false;
                }
            }
        }
    }
}