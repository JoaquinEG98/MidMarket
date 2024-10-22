using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class Compra : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ICarritoService _carritoService;

        public Compra()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _carritoService = Global.Container.Resolve<ICarritoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
                }
            }
        }

        protected void AgregarAccionAlCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el Id de la acción desde CommandArgument
                var button = (Button)sender;
                int accionId = int.Parse(button.CommandArgument);

                // Buscar la acción por su Id
                var accion = _activoService.GetAcciones().FirstOrDefault(a => a.Id == accionId);

                if (accion != null)
                {
                    // Llamar al servicio para insertar la acción en el carrito
                    _carritoService.InsertarCarrito(accion);

                    // Mostrar un mensaje de éxito
                    AlertHelper.MostrarMensaje(this, $"La acción {accion.Nombre} ha sido agregada al carrito.");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al agregar la acción al carrito: {ex.Message}");
            }
        }

        protected void AgregarBonoAlCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el Id del bono desde CommandArgument
                var button = (Button)sender;
                int bonoId = int.Parse(button.CommandArgument);

                // Buscar el bono por su Id
                var bono = _activoService.GetBonos().FirstOrDefault(b => b.Id == bonoId);

                if (bono != null)
                {
                    // Llamar al servicio para insertar el bono en el carrito
                    _carritoService.InsertarCarrito(bono);

                    // Mostrar un mensaje de éxito
                    AlertHelper.MostrarMensaje(this, $"El bono {bono.Nombre} ha sido agregado al carrito.");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al agregar el bono al carrito: {ex.Message}");
            }
        }
    }
}
