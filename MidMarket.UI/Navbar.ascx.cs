using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class Navbar : System.Web.UI.UserControl
    {
        private readonly ISessionManager _sessionManager;

        public Navbar()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            usuariosDropdown.Visible = false;
            familiasDropdown.Visible = false;
            permisosDropdown.Visible = false;
            administracionBD.Visible = false;
            bitacora.Visible = false;

            Cliente cliente = _sessionManager.Get<Cliente>("Usuario");

            foreach (var permiso in cliente.Permisos)
            {
                if (permiso.Permiso == Entities.Enums.Permiso.EsFamilia && permiso.Nombre == "Webmaster")
                {
                    bitacora.Visible = true;
                }
            }
        }
    }
}