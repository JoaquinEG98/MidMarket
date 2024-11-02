using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using MidMarket.UI.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class Transacciones : System.Web.UI.Page
    {
        private readonly ICompraService _compraService;
        private readonly IVentaService _ventaService;
        private readonly GeneradorPdf _generadorPdfService;

        public IList<TransaccionCompra> Compras { get; set; }
        public IList<TransaccionVenta> Ventas { get; set; }

        public string ComprasJson
        {
            get
            {
                return JsonConvert.SerializeObject(Compras, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }
        public string VentasJson
        {
            get
            {
                return JsonConvert.SerializeObject(Ventas, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }

        public Transacciones()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
            _ventaService = Global.Container.Resolve<IVentaService>();
            _generadorPdfService = new GeneradorPdf();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarCompras();
                    CargarVentas();
                    DescargarFacturaCompra();
                    DescargarFacturaVenta();
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}.");
            }
        }

        private void CargarCompras()
        {
            Compras = _compraService.GetCompras(true);
            ViewState["ComprasJson"] = JsonConvert.SerializeObject(Compras, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private void CargarVentas()
        {
            Ventas = _ventaService.GetVentas();
            ViewState["VentasJson"] = JsonConvert.SerializeObject(Ventas, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private void DescargarFacturaCompra()
        {
            if (Request.QueryString["descargarfacturacompra"] != null && int.TryParse(Request.QueryString["descargarfacturacompra"], out int compraId))
            {
                var compra = Compras.Where(x => x.Id == compraId).FirstOrDefault();
                if (compra != null)
                {
                    var bytes = _generadorPdfService.GenerarPdfCompra(compra);

                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", $"attachment; filename=Factura_Compra_{compra.Id}.pdf");
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        private void DescargarFacturaVenta()
        {
            if (Request.QueryString["descargarfacturaventa"] != null && int.TryParse(Request.QueryString["descargarfacturaventa"], out int ventaId))
            {
                var venta = Ventas.Where(x => x.Id == ventaId).FirstOrDefault();
                if (venta != null)
                {
                    var bytes = _generadorPdfService.GenerarPdfVenta(venta);

                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", $"attachment; filename=Factura_Venta_{venta.Id}.pdf");
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}
