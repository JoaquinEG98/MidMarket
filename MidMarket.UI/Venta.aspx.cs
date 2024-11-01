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
            if (!IsPostBack)
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.VenderAccion))
                {
                    Response.Redirect("Default.aspx");
                }

                try
                {
                    CargarComprasConsolidadas();
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}");
                    Response.Redirect("Default.aspx");
                }
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

        private void CargarComprasConsolidadas()
        {
            // Obtiene todas las transacciones de compra
            var comprasOriginales = _compraService.GetCompras(false);

            // Agrupa y consolida los activos por Id del activo
            var activosAgrupados = AgruparActivosPorId(comprasOriginales);
            Compras = ConsolidarActivosPorId(activosAgrupados);

            // Realiza el DataBind del Repeater para mostrar los datos consolidados
            rptTransacciones.DataSource = Compras;
            rptTransacciones.DataBind();
        }

        private Dictionary<int, (string Nombre, decimal Cantidad, decimal Precio, decimal ValorNominal)> AgruparActivosPorId(List<TransaccionCompra> compras)
        {
            var activosAgrupados = new Dictionary<int, (string Nombre, decimal Cantidad, decimal Precio, decimal ValorNominal)>();

            foreach (var compra in compras)
            {
                foreach (var detalle in compra.Detalle)
                {
                    int idActivo = detalle.Activo.Id;
                    string nombreActivo = detalle.Activo.Nombre;

                    if (detalle.Activo is Accion accion)
                    {
                        if (activosAgrupados.ContainsKey(idActivo))
                        {
                            var (nombre, cantidad, precio, valorNominal) = activosAgrupados[idActivo];
                            activosAgrupados[idActivo] = (nombre, cantidad + detalle.Cantidad, precio + (accion.Precio * detalle.Cantidad), valorNominal);
                        }
                        else
                        {
                            activosAgrupados[idActivo] = (nombreActivo, detalle.Cantidad, accion.Precio * detalle.Cantidad, 0);
                        }
                    }
                    else if (detalle.Activo is Bono bono)
                    {
                        if (activosAgrupados.ContainsKey(idActivo))
                        {
                            var (nombre, cantidad, precio, valorNominal) = activosAgrupados[idActivo];
                            activosAgrupados[idActivo] = (nombre, cantidad + detalle.Cantidad, precio, valorNominal + (bono.ValorNominal * detalle.Cantidad));
                        }
                        else
                        {
                            activosAgrupados[idActivo] = (nombreActivo, detalle.Cantidad, 0, bono.ValorNominal * detalle.Cantidad);
                        }
                    }
                }
            }

            return activosAgrupados;
        }

        private List<TransaccionCompra> ConsolidarActivosPorId(Dictionary<int, (string Nombre, decimal Cantidad, decimal Precio, decimal ValorNominal)> activosAgrupados)
        {
            var activosConsolidados = new List<DetalleCompra>();

            foreach (var activo in activosAgrupados)
            {
                var detalleCompra = new DetalleCompra
                {
                    Activo = new Activo { Id = activo.Key, Nombre = activo.Value.Nombre },
                    Cantidad = (int)activo.Value.Cantidad
                };

                if (activo.Value.Precio > 0)
                {
                    detalleCompra.Activo = new Accion
                    {
                        Id = activo.Key,
                        Nombre = activo.Value.Nombre,
                        Precio = activo.Value.Precio / activo.Value.Cantidad
                    };
                }
                else
                {
                    detalleCompra.Activo = new Bono
                    {
                        Id = activo.Key,
                        Nombre = activo.Value.Nombre,
                        ValorNominal = activo.Value.ValorNominal / activo.Value.Cantidad
                    };
                }

                activosConsolidados.Add(detalleCompra);
            }

            return new List<TransaccionCompra> { new TransaccionCompra { Detalle = activosConsolidados } };
        }

    }
}
