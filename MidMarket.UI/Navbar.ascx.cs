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
            Cliente cliente = _sessionManager.Get<Cliente>("Usuario");

            OcultarMenu();
            AsignarMenuPermisos(cliente);
        }

        private void OcultarMenu()
        {
            accionesDropDown.Visible = false;
            bonosDropDown.Visible = false;
            usuariosDropdown.Visible = false;
            familiasDropdown.Visible = false;
            permisosDropdown.Visible = false;
            administracionBD.Visible = false;
            bitacora.Visible = false;

        }

        private void AsignarMenuPermisos(Cliente cliente)
        {
            if (cliente.Email == "administrador@midmarket.com.ar")
            {
                accionesDropDown.Visible = true;
                bonosDropDown.Visible = true;
            }
            else if (cliente.Email == "webmaster@midmarket.com.ar")
            {
                bitacora.Visible = true;
            }


            //foreach (var permiso in cliente.Permisos)
            //{
            //    if (permiso.Permiso == Entities.Enums.Permiso.EsFamilia && permiso.Nombre == "Webmaster")
            //    {
            //        bitacora.Visible = true;
            //    }
            //    else if (permiso.Permiso == Entities.Enums.Permiso.EsFamilia && permiso.Nombre == "Administrador Financiero")
            //    {
            //        accionesDropDown.Visible = true;
            //        bonosDropDown.Visible = true;
            //    }
            //}
        }
    }
}