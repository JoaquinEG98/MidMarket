using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Data.SqlClient;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class ModificarAccion : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public Accion Accion { get; set; }
        private int _accionId;

        public ModificarAccion()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ModificarAccion))
                Response.Redirect("Default.aspx");

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (!IsPostBack)
            {
                try
                {
                    _accionId = int.Parse(Request.QueryString["id"]);
                    ViewState["AccionId"] = _accionId;
                    CargarAccion();
                }
                catch (SqlException)
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"{ex.Message}");
                    Response.Redirect("AdministrarAcciones.aspx");
                }
            }
            else
            {
                _accionId = (int)ViewState["AccionId"];
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                string nombreAccion = ValidarAcciones.Nombre;
                string simboloAccion = ValidarAcciones.Simbolo;
                decimal precioAccion = ValidarAcciones.Precio;

                GuardarAccion(nombreAccion, simboloAccion, precioAccion);

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_25")} {nombreAccion}");
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        private void GuardarAccion(string nombreAccion, string simboloAccion, decimal precioAccion)
        {
            var accion = _activoService.GetAcciones().FirstOrDefault(x => x.Id == _accionId);

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (accion == null)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_26")}");
                return;
            }

            accion.Nombre = nombreAccion;
            accion.Simbolo = simboloAccion;
            accion.Precio = precioAccion;

            _activoService.ModificarAccion(accion);
        }

        private void CargarAccion()
        {
            Accion = _activoService.GetAcciones().FirstOrDefault(x => x.Id == _accionId);

            if (Accion == null)
            {
                Response.Redirect("AdministrarAcciones.aspx");
                return;
            }

            ValidarAcciones.Nombre = Accion.Nombre;
            ValidarAcciones.Simbolo = Accion.Simbolo;
            ValidarAcciones.Precio = Accion.Precio;
        }
    }
}
