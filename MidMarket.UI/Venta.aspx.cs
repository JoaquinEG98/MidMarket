using MidMarket.Business.Interfaces;
using MidMarket.Entities;
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
            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        private void CargarCompras()
        {
            // Obtiene todas las transacciones de compra
            var compras = _compraService.GetCompras(false);
            rptTransacciones.DataSource = compras;
            rptTransacciones.DataBind();
        }

        protected void VenderActivo_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                int activoId = int.Parse(button.CommandArgument);

                // Encuentra la fila del Repeater y obtiene el TextBox de cantidad
                RepeaterItem item = (RepeaterItem)button.NamingContainer;
                TextBox txtCantidadVender = (TextBox)item.FindControl("txtCantidadVender");

                int cantidad = int.Parse(txtCantidadVender.Text);

                // Lógica de venta
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                if (clienteLogueado != null && cantidad > 0)
                {
                    //bool resultado = _ventaService.VenderAccion(clienteLogueado.Id, activoId, cantidad);

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
