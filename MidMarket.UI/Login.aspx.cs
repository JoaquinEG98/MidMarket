using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.UI.Helpers;
using System;
using System.Configuration;
using System.Data.SqlClient;
using Unity;

namespace MidMarket.UI
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUsuarioService _usuarioService;
        private readonly ITraduccionService _traduccionService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public Login()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cliente = _sessionManager.Get<Cliente>("Usuario");

                if (cliente != null)
                    Response.Redirect("MenuPrincipal.aspx");
            }

            VerificarIdioma();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                bool loginValido = _digitoVerificadorService.ValidarDigitosVerificadores("Cliente") &&
                    _digitoVerificadorService.ValidarDigitosVerificadores("UsuarioPermiso") &&
                    _digitoVerificadorService.ValidarDigitosVerificadores("FamiliaPatente") &&
                    _digitoVerificadorService.ValidarDigitosVerificadores("Permiso");

                if (loginValido)
                {
                    Cliente cliente = _usuarioService.Login(txtEmail.Value, txtPassword.Value);

                    _sessionManager.Set("Usuario", cliente);

                    Response.Redirect("MenuPrincipal.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    var usuario = ConfigurationManager.AppSettings["usuario"];
                    var password = ConfigurationManager.AppSettings["password"];

                    if (txtEmail.Value == usuario && txtPassword.Value == password)
                    {
                        var cliente = new Cliente()
                        {
                            Debug = true,
                            RazonSocial = "Webmaster DEBUG",
                        };

                        _sessionManager.Set("Usuario", cliente);

                        Response.Redirect("MenuPrincipal.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                    else
                    {
                        lblError.Text = $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_42")}";
                        lblError.Visible = true;
                    }

                }
            }
            catch (SqlException)
            {
                lblError.Text = $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}";
                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = $"{ex.Message}";
                lblError.Visible = true;
            }
        }

        private void VerificarIdioma()
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            var traducciones = _traduccionService.ObtenerTraducciones(idioma);
            ScriptHelper.TraducirPagina(this.Page, traducciones, _sessionManager);
        }
    }
}
