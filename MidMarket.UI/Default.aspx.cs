using MidMarket.Business.Interfaces;
using System;
using System.Web.UI;
using Unity;

namespace MidMarket.UI
{
    public partial class _Default : Page
    {
        private readonly IUsuarioService _usuarioService;

        public _Default()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
        }
    }
}