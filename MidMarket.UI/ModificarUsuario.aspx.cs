using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class ModificarUsuario : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public Cliente Usuario { get; set; }
        private int _usuarioId { get; set; }

        public ModificarUsuario()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ModificacionUsuario))
                Response.Redirect("Default.aspx");

            try
            {
                _usuarioId = int.Parse(Request.QueryString["id"]);
                CargarUsuario();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
                Response.Redirect("AdministrarUsuarios.aspx");
            }
        }

        private void CargarUsuario()
        {
            Usuario = _usuarioService.GetClientes().FirstOrDefault(x => x.Id == _usuarioId);
            if (Usuario == null)
            {
                Response.Redirect("Usuarios.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                    return;

                var idioma = _sessionManager.Get<IIdioma>("Idioma");

                string emailUsuario = Request.Form["emailUsuario"];
                string razonSocialUsuario = Request.Form["razonSocialUsuario"];
                string cuitUsuario = Request.Form["cuitUsuario"];

                GuardarUsuario(emailUsuario, razonSocialUsuario, cuitUsuario);
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_31")} {emailUsuario}");
                CargarUsuario();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        private void GuardarUsuario(string email, string razonSocial, string cuit)
        {
            var usuario = new Cliente()
            {
                Id = Usuario.Id,
                Email = email,
                Password = Usuario.Password,
                RazonSocial = razonSocial,
                CUIT = cuit,
                Bloqueo = Usuario.Bloqueo,
                Puntaje = Usuario.Puntaje,
            };

            _usuarioService.ModificarUsuario(usuario);
        }
    }
}