using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class ModificarAccion : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        public Accion Accion { get; set; }
        private int _accionId { get; set; }

        public ModificarAccion()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
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
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}");
                Response.Redirect("AdministrarAcciones.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreAccion = Request.Form["nombreAccion"];
                string simboloAccion = Request.Form["simboloAccion"];
                decimal precioAccion = decimal.Parse(Request.Form["precioAccion"]);

                GuardarAccion(nombreAccion, simboloAccion, precioAccion);
                AlertHelper.MostrarMensaje(this, $"Acción {nombreAccion} modificada correctamente.");
                CargarAccion();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al modificar la acción: {ex.Message}");
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
                AlertHelper.MostrarMensaje(this, "Acción no encontrada.");
                Response.Redirect("AdministrarAcciones.aspx");
            }
        }
    }
}