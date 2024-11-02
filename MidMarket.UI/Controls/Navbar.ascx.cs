using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using System;
using System.Linq;
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

            var esAdmin = cliente.Permisos.Any(permiso => permiso.Nombre == "Webmaster" || permiso.Nombre == "Administrador Financiero");
            if (esAdmin)
            {
                carritoDropdown.Visible = false;
                misTransaccionesDropdown.Visible = false;
                menuPrincipalDropdown.Visible = false;
            }
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
            transaccionesDropdown.Visible = false;
            //carritoDropdown.Visible = false;
            //misTransaccionesDropdown.Visible = false;
            portafolioDrowndown.Visible = false;

            comprarAccionLink.Visible = false;
            venderAccionLink.Visible = false;
            administrarAccionesLink.Visible = false;
            altaAccionesLink.Visible = false;
            administrarBonosLink.Visible = false;
            altaBonosLink.Visible = false;
            usuariosLink.Visible = false;
            altaUsuarioLink.Visible = false;

            asignarPatentesLink.Visible = false;
            asignarFamiliasLink.Visible = false;
            desasignarPatentesLink.Visible = false;
            desasignarFamiliasLink.Visible = false;

            administracionFamiliasLink.Visible = false;
            altaFamiliaLink.Visible = false;
        }

        private void AsignarMenuPermisos(Cliente cliente)
        {
            foreach (var permiso in cliente.Permisos)
            {
                if (permiso.Permiso == Entities.Enums.Permiso.EsFamilia)
                {
                    foreach (var permisoHijo in permiso.Hijos)
                    {
                        AsignarPermiso(permisoHijo);
                    }
                }
                else
                {
                    AsignarPermiso(permiso);
                }
            }
        }

        private void AsignarPermiso(Componente permiso)
        {
            switch (permiso.Permiso)
            {
                case Entities.Enums.Permiso.VisualizarBitacora:
                    bitacora.Visible = true;
                    break;

                case Entities.Enums.Permiso.ComprarAccion:
                    transaccionesDropdown.Visible = true;
                    comprarAccionLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.ComprarBono:
                    transaccionesDropdown.Visible = true;
                    comprarAccionLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.VenderAccion:
                    transaccionesDropdown.Visible = true;
                    venderAccionLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.VenderBono:
                    transaccionesDropdown.Visible = true;
                    venderAccionLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AdministracionFamilias:
                    familiasDropdown.Visible = true;
                    administracionFamiliasLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AltaFamilia:
                    familiasDropdown.Visible = true;
                    altaFamiliaLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AsignarPatentes:
                    permisosDropdown.Visible = true;
                    asignarPatentesLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.DesasignarPatentes:
                    permisosDropdown.Visible = true;
                    desasignarPatentesLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AsignarFamilias:
                    permisosDropdown.Visible = true;
                    asignarFamiliasLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.DesasignarFamilias:
                    permisosDropdown.Visible = true;
                    desasignarFamiliasLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AdministracionUsuarios:
                    usuariosDropdown.Visible = true;
                    usuariosLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AltaUsuario:
                    usuariosDropdown.Visible = true;
                    altaUsuarioLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AdministracionBaseDeDatos:
                    administracionBD.Visible = true;
                    break;

                case Entities.Enums.Permiso.ModificarAccion:
                    accionesDropDown.Visible = true;
                    administrarAccionesLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.ModificarBono:
                    bonosDropDown.Visible = true;
                    administrarBonosLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AltaBono:
                    bonosDropDown.Visible = true;
                    altaBonosLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.AltaAccion:
                    accionesDropDown.Visible = true;
                    altaAccionesLink.Visible = true;
                    break;

                case Entities.Enums.Permiso.VisualizarPortafolio:
                    portafolioDrowndown.Visible = true;
                    break;
            }
        }
    }
}
