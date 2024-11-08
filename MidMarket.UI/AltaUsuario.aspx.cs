using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Data.SqlClient;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaUsuario : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;


        public AltaUsuario()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AltaUsuario))
                Response.Redirect("Default.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var idioma = _sessionManager.Get<IIdioma>("Idioma");

                try
                {

                    Cliente cliente = new Cliente()
                    {
                        Email = ValidarUsuarioControl.EmailValue,
                        Password = ValidarUsuarioControl.PasswordValue,
                        RazonSocial = ValidarUsuarioControl.RazonSocialValue,
                        CUIT = ValidarUsuarioControl.CUITValue
                    };

                    _usuarioService.RegistrarUsuario(cliente);

                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_10")}");
                    LimpiarCampos();
                }
                catch (SqlException)
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"{ex.Message}.");
                }
            }
        }

        private void LimpiarCampos()
        {
            ValidarUsuarioControl.LimpiarCampos();
        }
    }
}