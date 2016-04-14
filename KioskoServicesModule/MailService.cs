
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
namespace Decktra.PubliPuntoEstacion.KioskoServicesModule
{
    public class MailService
    {
        private string _kioskoID;
        private string _pwdMail;

        public MailService(string kioskoID, string pwdMail)
        {
            this._kioskoID = kioskoID;
            this._pwdMail = pwdMail;
        }

        public void SendMailFromContactanos(string from, string to, string nombre, string telefono, string comentario)
        {
            string msg = "Redactado Por: " + nombre + "\n";
            msg += "Para: " + to + "\n";
            msg += "Email: " + from + "\n";
            msg += "Telefono: " + telefono + "\n";
            msg += "Comentario: " + comentario;

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("info@cuponexpress.com.ve", _pwdMail),
                EnableSsl = true
            };

            var subject = "Contacto desde " + _kioskoID;
            Task.Run(() =>
            {
                client.Send("info@cuponexpress.com.ve", "info@cuponexpress.com.ve", subject, msg);
            }, CancellationToken.None);
        }
    }
}
