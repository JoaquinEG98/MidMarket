using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class Venta : System.Web.UI.Page
    {
        private readonly ICompraService _compraService;
        private readonly IVentaService _ventaService;
        private readonly ISessionManager _sessionManager;

        public List<TransaccionCompra> Compras { get; set; }

        public Venta()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
            _ventaService = Global.Container.Resolve<IVentaService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.VenderAccion) || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.VenderBono))
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                try
                {
                    CargarCompras();
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}.");
                }
            }
        }

        private void CargarCompras()
        {
            // Obtiene todas las compras del cliente
            Compras = _compraService.GetCompras(false);
        }

        protected void VenderActivo_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                int activoId = int.Parse(button.CommandArgument);

                // Obtiene el TextBox de cantidad a vender en la misma fila
                var cantidadTextBox = (TextBox)button.NamingContainer.FindControl("txtCantidadVender");
                int cantidad = int.Parse(cantidadTextBox.Text);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                if (clienteLogueado != null && cantidad > 0)
                {
                    //// Obtiene el activo para verificar su tipo (acción o bono)
                    //var activo = _compraService.GetActivoById(activoId);

                    //bool resultado;

                    //if (activo is Accion)
                    //{
                    //    resultado = _ventaService.VenderAccion(clienteLogueado.Id, activoId, cantidad);
                    //}
                    //else if (activo is Bono)
                    //{
                    //    resultado = _ventaService.VenderBono(clienteLogueado.Id, activoId, cantidad);
                    //}
                    //else
                    //{
                    //    throw new Exception("Tipo de activo no reconocido.");
                    //}

                    //if (resultado)
                    //{
                    //    AlertHelper.MostrarToast(this, $"Venta realizada con éxito.");
                    //    CargarCompras(); // Recarga la tabla después de la venta
                    //}
                    //else
                    //{
                    //    AlertHelper.MostrarModal(this, $"Error al realizar la venta.");
                    //}
                }
                else
                {
                    AlertHelper.MostrarModal(this, "Cantidad inválida.");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al realizar la venta: {ex.Message}");
            }
        }
    }
}
