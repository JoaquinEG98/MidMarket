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
                MostrarFormularioSolicitud();
            }
            else
            {
                ProcesarToken(token);
            }
        }

        private void MostrarFormularioSolicitud()
        {
            formSolicitud.Visible = true;
        }

        private void ProcesarToken(string token)
        {
            if (!EsTokenValido(token))
            {
                MostrarFormularioSolicitud();
                lblMensaje.Text = "El enlace de restauración ha expirado o es inválido. Solicitá un nuevo enlace.";
                lblMensaje.CssClass = "error-label";
                lblMensaje.Visible = true;
                return;
            }

            string email = GetEmailPorToken(token);
            string nuevaPassword = GenerarPasswordRandom();

            ActualizarPassword(email, nuevaPassword);
            EnviarMailNuevaPassword(email, nuevaPassword);

            EliminarToken(token);

            MostrarFormularioSolicitud();
            lblMensaje.Text = "Se ha generado una nueva contraseña y se ha enviado a su correo.";
            lblMensaje.CssClass = "success-label";
            lblMensaje.Visible = true;
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

            lblMensaje.Text = "Se ha enviado un enlace de restauración a tu correo.";
            lblMensaje.CssClass = "success-label";
            lblMensaje.Visible = true;

            ValidarEmailControl.Email = string.Empty;
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
            string subject = "MidMarket - Restauración de Contraseña";
            string body = $"Hola, {email}.\n\nHacé clic en el siguiente enlace para restaurar tu contraseña:\n{link}\n\nEste enlace expirará en 15 minutos.";

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

        private bool EsTokenValido(string token)
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

        private string GetEmailPorToken(string token)
        {
            string json = File.ReadAllText(Server.MapPath(TokenPath));
            List<TokenEmailDTO> tokens = JsonConvert.DeserializeObject<List<TokenEmailDTO>>(json) ?? new List<TokenEmailDTO>();

            TokenEmailDTO tokenInfo = tokens.FirstOrDefault(t => t.Token == token);
            return tokenInfo?.Email;
        }

        private void EliminarToken(string token)
        {
            string json = File.ReadAllText(Server.MapPath(TokenPath));
            List<TokenEmailDTO> tokens = JsonConvert.DeserializeObject<List<TokenEmailDTO>>(json) ?? new List<TokenEmailDTO>();

            tokens.RemoveAll(t => t.Token == token);
            File.WriteAllText(Server.MapPath(TokenPath), JsonConvert.SerializeObject(tokens, Formatting.Indented));
        }

        private void ActualizarPassword(string email, string newPassword)
        {
            _usuarioService.ReestablecerPassword(email, newPassword);
        }

        private void EnviarMailNuevaPassword(string email, string newPassword)
        {
            string subject = "MidMarket - Nueva contraseña generada";
            string body = $"Hola, {email}.\n\nTu nueva contraseña es: {newPassword}\n\nPor favor, inicia sesión y cambia tu contraseña en la sección de cambio de contraseña.";

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

        private string GenerarPasswordRandom()
        {
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "!?.#$%";

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