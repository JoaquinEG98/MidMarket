using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Observer;
using MidMarket.UI.Helpers;
using MidMarket.UI.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class ReestablecerPassword : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;
        private readonly EnvioEmail _emailService;

        private const string TokenPath = "~/App_Data/tokens.json";
        private readonly string UrlSitio = ConfigurationManager.AppSettings["UrlSitio"];

        public ReestablecerPassword()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
            _emailService = new EnvioEmail();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                TraducirPagina();

                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                    MostrarFormularioSolicitud();
                else
                    ProcesarToken(token);
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
                Response.Redirect("Default.aspx");
            }
        }

        private void TraducirPagina()
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");
            var traducciones = _traduccionService.ObtenerTraducciones(idioma);
            ScriptHelper.TraducirPagina(this.Page, traducciones, _sessionManager);
        }

        private void MostrarFormularioSolicitud() => formSolicitud.Visible = true;

        private void ProcesarToken(string token)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (!EsTokenValido(token))
            {
                MostrarError($"{_traduccionService.ObtenerMensaje(idioma, "MSJ_36")}");
                return;
            }

            string email = GetEmailPorToken(token);
            string nuevaPassword = Encriptacion.GenerarPasswordRandom();

            ActualizarPassword(email, nuevaPassword);
            _emailService.RealizarEnvioEmail(email, "MidMarket - Nueva contraseña generada", $"Tu nueva contraseña es: {nuevaPassword}");

            EliminarToken(token);
            MostrarMensaje($"{_traduccionService.ObtenerMensaje(idioma, "MSJ_37")}");
        }

        protected void btnReestablecer_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            string email = ValidarEmailControl.Email;
            string token = GenerarToken();
            GuardarToken(new TokenEmailDTO { Email = email, Token = token, FechaExpiracion = DateTime.Now.AddMinutes(15) });
            _emailService.RealizarEnvioEmail(email, "MidMarket - Restauración de Contraseña", $"{UrlSitio}/ReestablecerPassword.aspx?token={token}");

            MostrarMensaje($"{_traduccionService.ObtenerMensaje(idioma, "MSJ_35")}");
            ValidarEmailControl.Email = string.Empty;
        }

        private void GuardarToken(TokenEmailDTO tokenInfo)
        {
            var tokens = LeerTokensJson();
            tokens.Add(tokenInfo);
            GuardarTokensJson(tokens);
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