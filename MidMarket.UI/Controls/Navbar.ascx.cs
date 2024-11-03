﻿using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.Observer;
using System;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class Navbar : System.Web.UI.UserControl, IObserver
    {
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public Navbar()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Cliente cliente = _sessionManager.Get<Cliente>("Usuario");
            cliente.SuscribirObservador(this);

            if (!IsPostBack)
            {
                OcultarMenu();
                AsignarMenuPermisos(cliente);

                var idiomas = _traduccionService.ObtenerIdiomas();
                idiomaRepeater.DataSource = idiomas;
                idiomaRepeater.DataBind();

                var esAdmin = cliente.Permisos.Any(permiso => permiso.Nombre == "Webmaster" || permiso.Nombre == "Administrador Financiero");
                if (esAdmin)
                {
                    carritoDropdown.Visible = false;
                    misTransaccionesDropdown.Visible = false;
                    menuPrincipalDropdown.Visible = false;
                }
            }
            else
            {
                // Detecta si el postback viene del cambio de idioma
                string eventTarget = Request["__EVENTTARGET"];
                string eventArgument = Request["__EVENTARGUMENT"];

                if (eventTarget == "ChangeLanguage" && int.TryParse(eventArgument, out int idiomaId))
                {
                    var idioma = _traduccionService.ObtenerIdiomas().Where(x => x.Id == idiomaId).FirstOrDefault();
                    //Cliente cliente = _sessionManager.Get<Cliente>("Usuario");
                    cliente.CambiarIdioma(idioma);
                }
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

        public void UpdateLanguage(IIdioma idioma)
        {
            var traducciones = _traduccionService.ObtenerTraducciones(idioma);

            // Convertir las traducciones a JSON
            var traduccionesJson = Newtonsoft.Json.JsonConvert.SerializeObject(traducciones.ToDictionary(t => t.Key, t => t.Value.Texto));

            // Registrar el script en el cliente para definir la variable de traducciones
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetTranslations", $"var traducciones = {traduccionesJson};", true);
        }
    }
}
