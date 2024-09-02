using MidMarket.Business;
using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using System.Web.UI;
using Unity;

namespace MidMarket.UI
{
    public partial class _Default : Page
    {
        public Cliente Cliente { get; set; }

        private readonly ISessionManager _sessionManager;
        private readonly IUsuarioService _usuarioService;

        public _Default()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Cliente = _sessionManager.Get<Cliente>("Usuario");

                    if (Cliente == null)
                        Response.Redirect("Default.aspx");
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}