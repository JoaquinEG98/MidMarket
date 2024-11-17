using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.UI.Helpers;
using MidMarket.UI.WebServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class Carrito : System.Web.UI.Page
    {
        private const string CarritoSessionKey = "MiCarrito";

        public List<Entities.Carrito> MiCarrito
        {
            get
            {
                return _sessionManager.Get<List<Entities.Carrito>>(CarritoSessionKey);
            }
            set
            {
                _sessionManager.Set(CarritoSessionKey, value);
            }
        }

        private readonly ISessionManager _sessionManager;
        private readonly ICarritoService _carritoService;
        private readonly ICompraService _compraService;
        private readonly CalcularCarrito _calcularCarritoService;
        private readonly EstadisticaActivos _estadisticaActivosService;
        private readonly ITraduccionService _traduccionService;
        private readonly GeneradorPdf _pdfService;
        private readonly EnvioEmail _emailService;

        public Carrito()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _compraService = Global.Container.Resolve<ICompraService>();
            _calcularCarritoService = new CalcularCarrito();
            _estadisticaActivosService = new EstadisticaActivos();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
            _pdfService = new GeneradorPdf();
            _emailService = new EnvioEmail();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                var cliente = _sessionManager.Get<Cliente>("Usuario");

                MiCarrito = _carritoService.GetCarrito(cliente.Id);

                if (MiCarrito == null || MiCarrito.Count == 0)
                {
                    divCarrito.Visible = false;
                    ltlCarritoVacio.Visible = true;
                }
                else
                {
                    divCarrito.Visible = true;
                    ltlCarritoVacio.Visible = false;

                    decimal total = 0;
                    foreach (var item in MiCarrito)
                    {
                        total += item.Total;
                    }

                    ViewState["TotalCarrito"] = total;

                    rptCarrito.DataSource = MiCarrito;
                    rptCarrito.DataBind();
                }
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        protected void rptCarrito_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    var carritoItem = (MidMarket.Entities.Carrito)e.Item.DataItem;

                    var tdDetalle = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdDetalle");
                    var tdPrecioTasa = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdPrecioTasa");

                    if (tdDetalle != null && tdPrecioTasa != null)
                    {
                        if (carritoItem.Activo is Accion accion)
                        {
                            tdDetalle.InnerText = $"Símbolo: {accion.Simbolo}";
                            tdPrecioTasa.InnerText = $"${accion.Precio.ToString("N2")}";
                        }
                        else if (carritoItem.Activo is Bono bono)
                        {
                            tdDetalle.InnerText = $"Tasa: {bono.TasaInteres}%";
                            tdPrecioTasa.InnerText = $"${bono.ValorNominal.ToString("N2")}";
                        }
                    }
                }
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                decimal total = 0;

                if (e.CommandName == "CambiarCantidad")
                {
                    var argumentos = e.CommandArgument.ToString().Split(',');
                    int carritoId = int.Parse(argumentos[0]);
                    int cambioCantidad = int.Parse(argumentos[1]);

                    total = _calcularCarritoService.CalcularTotalCarrito(MiCarrito, "CambiarCantidad", carritoId, cambioCantidad);
                }
                else if (e.CommandName == "EliminarItem")
                {
                    int carritoId = int.Parse(e.CommandArgument.ToString());

                    total = _calcularCarritoService.CalcularTotalCarrito(MiCarrito, "EliminarItem", carritoId);
                }

                ViewState["TotalCarrito"] = total;

                if (MiCarrito.Count == 0)
                {
                    divCarrito.Visible = false;
                    ltlCarritoVacio.Visible = true;
                }
                else
                {
                    rptCarrito.DataSource = MiCarrito;
                    rptCarrito.DataBind();
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }


        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                var cliente = _sessionManager.Get<Cliente>("Usuario");

                if (MiCarrito == null || MiCarrito.Count == 0)
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_17")}");
                    return;
                }

                int compraId = _compraService.RealizarCompra(MiCarrito);

                MiCarrito.Clear();
                _carritoService.LimpiarCarrito(cliente.Id);

                divCarrito.Visible = false;
                ltlCarritoVacio.Visible = true;

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_18")}");

                CalcularComprasWebService();

                EnviarMailFactura(compraId);
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        private void CalcularComprasWebService()
        {
            _estadisticaActivosService.CalcularActivosMasCompradosCantidad();
            _estadisticaActivosService.CalcularActivosMasCompradosTotal();
        }

        private void EnviarMailFactura(int compraId)
        {
            var cliente = _sessionManager.Get<Cliente>("Usuario");

            var compra = _compraService.GetCompras(true).Where(x => x.Id == compraId).FirstOrDefault();
            var bytes = _pdfService.GenerarPdfCompra(compra);

            _emailService.RealizarEnvioEmailConAdjunto(cliente.Email, "MidMarket - Factura de Compra", "Acá está tu factura de compra.", bytes, $"Factura_Compra_{compraId}.pdf");
        }
    }
}