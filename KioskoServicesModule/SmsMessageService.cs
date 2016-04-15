
using Decktra.PubliPuntoEstacion.CoreApplication.Model;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace Decktra.PubliPuntoEstacion.KioskoServicesModule
{
    public class SmsMessageService
    {
        bool _envioSMS_ON;
        string _SMS_UserName, _SMS_Pwd;

        [Dependency]
        public ILoggerFacade Logger { get; set; }

        public SmsMessageService(bool envioSMS_ON, string SMS_UserName, string SMS_Pwd)
        {
            _envioSMS_ON = envioSMS_ON;
            _SMS_UserName = SMS_UserName;
            _SMS_Pwd = SMS_Pwd;
        }

        public async Task<bool> EnviarSmsToCliente(PromocionCupon promocionCupon, Usuario usuarioValidado, Promocion promocionSelected)
        {
            if ((_envioSMS_ON == false) || (string.IsNullOrEmpty(usuarioValidado.Movil))) return false;

            using (var client = new HttpClient())
            {
                string mensajeEnvio = BuildMensajeEnvio(promocionCupon, usuarioValidado, promocionSelected);
                if (string.IsNullOrEmpty(mensajeEnvio)) return false;

                string uriStr = string.Format("secure/insert.php?uname={0}&pass={1}&num={2}&msg={3}",
                    _SMS_UserName, _SMS_Pwd, usuarioValidado.Movil, Uri.EscapeUriString(mensajeEnvio));

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

        private string BuildMensajeEnvio(PromocionCupon promocionCupon, Usuario usuarioValidado, Promocion promocionSelected)
        {
            string file = "plantilla_sms.txt";
            string msg;
            if (!System.IO.File.Exists(file))
            {
                msg = "Cupón: {codigoCupon} Promo: {descripcionPromocion} Ver condiciones en http://www.cuponexpress.com.ve Gracias por usar CUPONEXPRESS";
                var stream = System.IO.File.CreateText(file);
                stream.Write(msg);
                stream.Close();
            }
            else
            {
                msg = System.IO.File.ReadAllText(file);
            }

            msg = msg.Replace("{codigoCupon}", promocionCupon.CodigoCanjeo).
                Replace("{descripcionPromocion}", promocionSelected.Descripcion).
                Replace("{fechaVigencia}", promocionCupon.FechaVigencia.Value.ToShortDateString()).
                Replace("{direccionLocal}", promocionSelected.EnteComercial.Direccion);

            return msg;
        }
    }
}
