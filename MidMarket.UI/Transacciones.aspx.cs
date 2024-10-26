using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
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
        public IList<TransaccionCompra> Compras { get; set; }
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

        public Transacciones()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarCompras();
                    DescargarFactura();
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}.");
            }
        }

        private void CargarCompras()
        {
            Compras = _compraService.GetCompras();
            ViewState["ComprasJson"] = JsonConvert.SerializeObject(Compras, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        private void DescargarFactura()
        {
            if (Request.QueryString["descargarfactura"] != null && int.TryParse(Request.QueryString["descargarfactura"], out int compraId))
            {
                var compra = Compras.Where(x => x.Id == compraId).FirstOrDefault();
                if (compra != null)
                {
                    _compraService.DescargarFacturaPdf(compra, Response);
                }
            }
        }
    }
}
