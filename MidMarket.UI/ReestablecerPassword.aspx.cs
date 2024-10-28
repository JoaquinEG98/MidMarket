using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.Entities.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                MostrarFormularioSolicitud();
            else
                ProcesarToken(token);
        }

        private void MostrarFormularioSolicitud() => formSolicitud.Visible = true;

        private void ProcesarToken(string token)
        {
            if (!EsTokenValido(token))
            {
                MostrarError("El enlace de restauración ha expirado o es inválido. Solicitá un nuevo enlace.");
                return;
            }

            string email = GetEmailPorToken(token);
            string nuevaPassword = Encriptacion.GenerarPasswordRandom();

            ActualizarPassword(email, nuevaPassword);
            EnviarCorreo(email, "MidMarket - Nueva contraseña generada", $"Tu nueva contraseña es: {nuevaPassword}");

            EliminarToken(token);
            MostrarMensaje("Se ha generado una nueva contraseña y se ha enviado a su correo.");
        }

        protected void btnReestablecer_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string email = ValidarEmailControl.Email;
            string token = GenerarToken();
            GuardarToken(new TokenEmailDTO { Email = email, Token = token, FechaExpiracion = DateTime.Now.AddMinutes(15) });
            EnviarCorreo(email, "MidMarket - Restauración de Contraseña", $"{UrlSitio}/ReestablecerPassword.aspx?token={token}");

            MostrarMensaje("Se ha enviado un enlace de restauración a tu correo.");
            ValidarEmailControl.Email = string.Empty;
        }

        private void GuardarToken(TokenEmailDTO tokenInfo)
        {
            var tokens = LeerTokensJson();
            tokens.Add(tokenInfo);
            GuardarTokensJson(tokens);
        }

        private void EnviarCorreo(string destinatario, string asunto, string mensaje)
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

        private bool EsTokenValido(string token)
        {
            return LeerTokensJson().Any(t => t.Token == token && t.FechaExpiracion > DateTime.Now);
        }

        private string GetEmailPorToken(string token)
        {
            return LeerTokensJson().FirstOrDefault(t => t.Token == token)?.Email;
        }

        private void EliminarToken(string token)
        {
            var tokens = LeerTokensJson().Where(t => t.Token != token).ToList();
            GuardarTokensJson(tokens);
        }

        private List<TokenEmailDTO> LeerTokensJson()
        {
            string filePath = Server.MapPath(TokenPath);
            if (!File.Exists(filePath)) return new List<TokenEmailDTO>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<TokenEmailDTO>>(json) ?? new List<TokenEmailDTO>();
        }

        private void GuardarTokensJson(List<TokenEmailDTO> tokens)
        {
            string filePath = Server.MapPath(TokenPath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(tokens, Formatting.Indented));
        }

        private void ActualizarPassword(string email, string newPassword)
        {
            _usuarioService.ReestablecerPassword(email, newPassword);
        }

        private string GenerarToken()
        {
            return Guid.NewGuid().ToString();
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "success-label";
            lblMensaje.Visible = true;
            MostrarFormularioSolicitud();
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "error-label";
            lblMensaje.Visible = true;
            MostrarFormularioSolicitud();
        }
    }
}
