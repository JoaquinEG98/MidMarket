using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using MidMarket.UI.WebServices;
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
        private readonly EstadisticaActivos _estadisticaActivosService;
        private readonly ITraduccionService _traduccionService;

        public List<TransaccionCompra> Compras { get; set; }

        public Venta()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
            _ventaService = Global.Container.Resolve<IVentaService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _estadisticaActivosService = new EstadisticaActivos();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.VenderAccion) || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.VenderBono))
                    Response.Redirect("Default.aspx");

                try
                {
                    CargarComprasConsolidadas();
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"{ex.Message}");
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void VenderActivo_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                int activoId = int.Parse(button.CommandArgument);

                RepeaterItem item = (RepeaterItem)button.NamingContainer;
                TextBox cantidadInput = (TextBox)item.FindControl("cantidadInput");

                int cantidad = int.Parse(cantidadInput.Text);


                DetalleVenta venta = new DetalleVenta()
                {
                    Activo = new Activo() { Id = activoId },
                    Cantidad = cantidad,
                    Precio = _ventaService.ObtenerUltimoPrecioActivo(activoId)
                };

                _ventaService.RealizarVenta(venta);
                CargarComprasConsolidadas();

                var idioma = _sessionManager.Get<IIdioma>("Idioma");

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_32")}");

                CalcularVentasWebService();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        private void CargarComprasConsolidadas()
        {
            var comprasOriginales = _compraService.GetCompras(false);

            if (comprasOriginales == null || comprasOriginales.Count == 0)
            {
                divVentas.Visible = false;
                ltlVentasVacias.Visible = true;
            }
            else
            {
                divVentas.Visible = true;
                ltlVentasVacias.Visible = false;

                var activosAgrupados = AgruparActivosPorId(comprasOriginales);
                Compras = ConsolidarActivosPorId(activosAgrupados);

                rptTransacciones.DataSource = Compras;
                rptTransacciones.DataBind();
            }
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

        private void CalcularVentasWebService()
        {
            _estadisticaActivosService.CalcularActivosMasVendidosCantidad();
            _estadisticaActivosService.CalcularActivosMasVendidosTotal();
        }
    }
}
