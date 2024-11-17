using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Services;

namespace MidMarket.UI.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class EnvioEmail : System.Web.Services.WebService
    {

        [WebMethod]
        public void RealizarEnvioEmail(string destinatario, string asunto, string mensaje)
        {
            using (var mail = new MailMessage("hello@demomailtrap.com", "joaquinezequielgonzalez98@gmail.com", asunto, mensaje))
            {
                mail.IsBodyHtml = false;
                using (var smtp = new SmtpClient("live.smtp.mailtrap.io", 587))
                {
                    smtp.Credentials = new NetworkCredential("smtp@mailtrap.io", "85c0349a01d11fd5d4230fbef10c1454");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        [WebMethod]
        public void RealizarEnvioEmailConAdjunto(string destinatario, string asunto, string mensaje, byte[] archivoAdjunto, string nombreArchivo)
        {
            using (var mail = new MailMessage("hello@demomailtrap.com", "joaquinezequielgonzalez98@gmail.com", asunto, mensaje))
            {
                mail.IsBodyHtml = false;

                if (archivoAdjunto != null && archivoAdjunto.Length > 0)
                {
                    var attachment = new Attachment(new MemoryStream(archivoAdjunto), nombreArchivo);
                    mail.Attachments.Add(attachment);
                }

                using (var smtp = new SmtpClient("live.smtp.mailtrap.io", 587))
                {
                    smtp.Credentials = new NetworkCredential("smtp@mailtrap.io", "85c0349a01d11fd5d4230fbef10c1454");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
