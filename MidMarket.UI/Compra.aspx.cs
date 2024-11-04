using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class Compra : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ICarritoService _carritoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public Compra()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ComprarAccion) || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ComprarBono))
                Response.Redirect("Default.aspx");

            if (!IsPostBack)
            {
                try
                {
                    var bonos = _activoService.GetBonos();
                    var acciones = _activoService.GetAcciones();

                    rptAcciones.DataSource = acciones;
                    rptAcciones.DataBind();

                    rptBonos.DataSource = bonos;
                    rptBonos.DataBind();
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"{ex.Message}.");
                }
            }
        }

        protected void AgregarAccionAlCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                var idioma = _sessionManager.Get<IIdioma>("Idioma");

                var button = (Button)sender;
                int accionId = int.Parse(button.CommandArgument);

                var accion = _activoService.GetAcciones().FirstOrDefault(a => a.Id == accionId);

                if (accion != null)
                {
                    _carritoService.InsertarCarrito(accion);
                    AlertHelper.MostrarToast(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_19")} {accion.Nombre}");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        protected void AgregarBonoAlCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                var idioma = _sessionManager.Get<IIdioma>("Idioma");

                var button = (Button)sender;
                int bonoId = int.Parse(button.CommandArgument);

                var bono = _activoService.GetBonos().FirstOrDefault(b => b.Id == bonoId);

                if (bono != null)
                {
                    _carritoService.InsertarCarrito(bono);
                    AlertHelper.MostrarToast(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_20")} {bono.Nombre}");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }
    }
}
