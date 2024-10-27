using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class ModificarAccion : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;

        public Accion Accion { get; set; }
        private int _accionId { get; set; }

        public ModificarAccion()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _accionId = int.Parse(Request.QueryString["id"]);
                CargarAccion();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}");
                Response.Redirect("AdministrarAcciones.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ModificarAccion))
                Response.Redirect("Default.aspx");

            try
            {
                string nombreAccion = Request.Form["nombreAccion"];
                string simboloAccion = Request.Form["simboloAccion"];
                decimal precioAccion = decimal.Parse(Request.Form["precioAccion"]);

                GuardarAccion(nombreAccion, simboloAccion, precioAccion);
                AlertHelper.MostrarModal(this, $"Acción {nombreAccion} modificada correctamente.");
                CargarAccion();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al modificar la acción: {ex.Message}");
            }
        }

        private void GuardarAccion(string nombreAccion, string simboloAccion, decimal precioAccion)
        {
            var accion = new Accion()
            {
                Id = Accion.Id,
                Id_Accion = Accion.Id_Accion,
                Nombre = nombreAccion,
                Simbolo = simboloAccion,
                Precio = precioAccion
            };

            _activoService.ModificarAccion(accion);
        }

        private void CargarAccion()
        {
            Accion = _activoService.GetAcciones().FirstOrDefault(x => x.Id == _accionId);
            if (Accion == null)
            {
                AlertHelper.MostrarModal(this, "Acción no encontrada.");
                Response.Redirect("AdministrarAcciones.aspx");
            }
        }
    }
}