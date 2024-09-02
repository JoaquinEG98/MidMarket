using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly ISessionManager _sessionManager;

        public Default()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cliente = _sessionManager.Get<Cliente>("Usuario");

                if (cliente != null)
                    Response.Redirect("MenuPrincipal.aspx");
            }
        }
    }
}