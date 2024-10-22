using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class Carrito : System.Web.UI.Page
    {
        public List<Entities.Carrito> MiCarrito { get; set; }


        private readonly ISessionManager _sessionManager;
        private readonly ICarritoService _carritoService;

        public Carrito()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _carritoService = Global.Container.Resolve<ICarritoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cliente = _sessionManager.Get<Cliente>("Usuario");

                MiCarrito = _carritoService.GetCarrito(cliente.Id);

                // Calcular el total considerando las cantidades y los precios/valores nominales
                decimal total = 0;
                foreach (var item in MiCarrito)
                {
                    if (item.Activo is Accion accion)
                    {
                        total += accion.Precio * item.Cantidad;
                    }
                    else if (item.Activo is Bono bono)
                    {
                        total += bono.ValorNominal * item.Cantidad;
                    }
                }

                // Asignar el total a una propiedad para mostrarlo en el frontend
                ViewState["TotalCarrito"] = total;

                // Asignar los datos al Repeater
                rptCarrito.DataSource = MiCarrito;
                rptCarrito.DataBind();
            }
        }

        protected void rptCarrito_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var carritoItem = (MidMarket.Entities.Carrito)e.Item.DataItem;

                // Encuentra los controles
                var tdDetalle = (HtmlTableCell)e.Item.FindControl("tdDetalle");
                var tdPrecioTasa = (HtmlTableCell)e.Item.FindControl("tdPrecioTasa");
                var cantidadInput = (HtmlInputGenericControl)e.Item.FindControl("cantidadInput"); // Actualización aquí

                if (tdDetalle != null && tdPrecioTasa != null)
                {
                    // Verifica si es Accion o Bono
                    if (carritoItem.Activo is Accion accion)
                    {
                        tdDetalle.InnerText = $"Símbolo: {accion.Simbolo}";
                        tdPrecioTasa.InnerText = $"${accion.Precio.ToString("F2")}";
                    }
                    else if (carritoItem.Activo is Bono bono)
                    {
                        tdDetalle.InnerText = $"Tasa: {bono.TasaInteres}%";
                        tdPrecioTasa.InnerText = $"${bono.ValorNominal.ToString("F2")}";
                    }
                }

                // Asigna la cantidad al input desde el backend
                if (cantidadInput != null)
                {
                    cantidadInput.Value = carritoItem.Cantidad.ToString();
                }
            }
        }

    }
}