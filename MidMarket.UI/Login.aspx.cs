using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUsuarioService _usuarioService;

        public Login()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = _usuarioService.Login(txtEmail.Value, txtPassword.Value);

                if (cliente != null)
                {
                    _sessionManager.Set("Usuario", cliente);

                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblError.Text = "El correo o la contraseña son incorrectos";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error al iniciar sesión: {ex.Message}";
                lblError.Visible = true;
            }
        }
    }
}