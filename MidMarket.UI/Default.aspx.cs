using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using System;
using System.Web.UI;
using Unity;

namespace MidMarket.UI
{
    public partial class _Default : Page
    {
        public Cliente Cliente { get; set; }
        private readonly IUsuarioService _usuarioService;

        public _Default()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cliente = Session["Usuario"] as Cliente;

                if (Cliente == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}