using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class CambiarPassword : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISessionManager _sessionManager;

        public CambiarPassword()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                Cliente cliente = new Cliente()
                {
                    Id = clienteLogueado.Id,
                    Password = passwordActual.Value
                };

                _usuarioService.CambiarPassword(cliente, ValidarPasswordControl.PasswordValue, confirmarPassword.Value);
                AlertHelper.MostrarModal(this, $"Contraseña cambiada con éxito");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cambiar contraseña: {ex.Message}");
            }
        }
    }
}