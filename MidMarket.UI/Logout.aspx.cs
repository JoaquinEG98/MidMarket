using MidMarket.Business.Interfaces;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class Logout : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;

        public Logout()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _usuarioService.Logout();
                Response.Redirect("Login.aspx");
            }
            catch (Exception)
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }

        }
    }
}