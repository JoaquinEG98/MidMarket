using MidMarket.Business.Interfaces;
using MidMarket.Entities.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Unity;

namespace MidMarket.UI
{
    public partial class ReestablecerPassword : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private const string TokenPath = "~/App_Data/tokens.json";
        private const string UrlSitio = "https://localhost:44339";

        public ReestablecerPassword()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];

            if (string.IsNullOrEmpty(token))
            {
                // Modo de Solicitud de Token (sin token en la URL)
                MostrarFormularioSolicitud();
            }
            else
            {
                // Modo de Verificación de Token (con token en la URL)
                ProcesarToken(token);
            }
        }

        private void MostrarFormularioSolicitud()
        {
            // Muestra el formulario para que el usuario ingrese su correo electrónico
            formSolicitud.Visible = true; // Asegúrate de tener un control form en el .aspx llamado formSolicitud
        }

        private void ProcesarToken(string token)
        {
            // Verificar si el token es válido y no ha expirado
            if (!IsTokenValid(token))
            {
                // Redirige si el token es inválido o ha expirado
                Response.Redirect("Default.aspx");
                return;
            }

            // Si el token es válido, generar una nueva contraseña
            string email = GetEmailByToken(token);
            string newPassword = GenerateRandomPassword();

            // Actualizar la contraseña del usuario y enviar por correo
            UpdateUserPassword(email, newPassword);
            SendNewPasswordEmail(email, newPassword);

            // Eliminar el token del archivo JSON
            RemoveToken(token);

            // Mensaje de éxito
            Response.Write("<script>alert('Se ha generado una nueva contraseña y se ha enviado a su correo.');</script>");
        }

        protected void btnReestablecer_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string email = ValidarEmailControl.Email;
            string token = GenerarToken();
            DateTime expiracion = DateTime.Now.AddMinutes(15);

            GuardarTokenJson(email, token, expiracion);
            EnviarCorreoRecuperacion(email, token);

            Response.Write("<script>alert('Se ha enviado un enlace de restauración a tu correo.');</script>");
        }

        private string GenerarToken()
        {
            return Guid.NewGuid().ToString();
        }

        private void GuardarTokenJson(string email, string token, DateTime expiracion)
        {
            var tokenInfo = new TokenEmailDTO
            {
                Email = email,
                Token = token,
                FechaExpiracion = expiracion
            };

            List<TokenEmailDTO> tokens;

            if (File.Exists(Server.MapPath(TokenPath)))
            {
                string json = File.ReadAllText(Server.MapPath(TokenPath));
                tokens = JsonConvert.DeserializeObject<List<TokenEmailDTO>>(json) ?? new List<TokenEmailDTO>();
            }
            else
            {
                tokens = new List<TokenEmailDTO>();
            }

            tokens.Add(tokenInfo);

            string updatedJson = JsonConvert.SerializeObject(tokens, Formatting.Indented);
            File.WriteAllText(Server.MapPath(TokenPath), updatedJson);
        }

        private void EnviarCorreoRecuperacion(string email, string token)
        {
            string link = $"{UrlSitio}/ReestablecerPassword.aspx?token={token}";
            string subject = "Restauración de Contraseña";
            string body = $"Hola,\n\nHaz clic en el siguiente enlace para restaurar tu contraseña:\n{link}\n\nEste enlace expirará en 15 minutos.";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("hello@demomailtrap.com");
                mail.To.Add("joaquinezequielgonzalez98@gmail.com");
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient("live.smtp.mailtrap.io", 587))
                {
                    smtp.Credentials = new NetworkCredential("smtp@mailtrap.io", "85c0349a01d11fd5d4230fbef10c1454");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private bool IsTokenValid(string token)
        {
            if (File.Exists(Server.MapPath(TokenPath)))
            {
                string json = File.ReadAllText(Server.MapPath(TokenPath));
                List<TokenEmailDTO> tokens = JsonConvert.DeserializeObject<List<TokenEmailDTO>>(json) ?? new List<TokenEmailDTO>();

                TokenEmailDTO tokenInfo = tokens.FirstOrDefault(t => t.Token == token && t.FechaExpiracion > DateTime.Now);
                return tokenInfo != null;
            }
            return false;
        }

        private string GetEmailByToken(string token)
        {
            string json = File.ReadAllText(Server.MapPath(TokenPath));
            List<TokenEmailDTO> tokens = JsonConvert.DeserializeObject<List<TokenEmailDTO>>(json) ?? new List<TokenEmailDTO>();

            TokenEmailDTO tokenInfo = tokens.FirstOrDefault(t => t.Token == token);
            return tokenInfo?.Email;
        }

        private void RemoveToken(string token)
        {
            string json = File.ReadAllText(Server.MapPath(TokenPath));
            List<TokenEmailDTO> tokens = JsonConvert.DeserializeObject<List<TokenEmailDTO>>(json) ?? new List<TokenEmailDTO>();

            tokens.RemoveAll(t => t.Token == token);
            File.WriteAllText(Server.MapPath(TokenPath), JsonConvert.SerializeObject(tokens, Formatting.Indented));
        }

        private void UpdateUserPassword(string email, string newPassword)
        {
            _usuarioService.ReestablecerPassword(email, newPassword);
        }

        private void SendNewPasswordEmail(string email, string newPassword)
        {
            string subject = "Nueva Contraseña Generada";
            string body = $"Hola,\n\nTu nueva contraseña es: {newPassword}\n\nPor favor, inicia sesión y cambia tu contraseña por una de tu preferencia.";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("hello@demomailtrap.com");
                mail.To.Add("joaquinezequielgonzalez98@gmail.com");
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient("live.smtp.mailtrap.io", 587))
                {
                    smtp.Credentials = new NetworkCredential("smtp@mailtrap.io", "85c0349a01d11fd5d4230fbef10c1454");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private string GenerateRandomPassword()
        {
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*";

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            password.Append(lowerChars[random.Next(lowerChars.Length)]);
            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(digitChars[random.Next(digitChars.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            string allChars = lowerChars + upperChars + digitChars + specialChars;
            while (password.Length < 8)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
        }
    }
}