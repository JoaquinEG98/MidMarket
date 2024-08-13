using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using System;
using Unity;

namespace MidMarket.UI
{
	public partial class Login : System.Web.UI.Page
	{
        private readonly IUsuarioService _usuarioService;

        public Login()
        {
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
                    Session["Usuario"] = cliente;
                    Response.Redirect("Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }
}