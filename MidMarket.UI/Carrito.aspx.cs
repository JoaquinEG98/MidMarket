using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using System;
using System.Collections.Generic;
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

        public Carrito()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _carritoService = Global.Container.Resolve<ICarritoService>();
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
                    if (item.Activo is Accion accion)
                    {
                        total += accion.Precio * item.Cantidad;
                    }
                    else if (item.Activo is Bono bono)
                    {
                        total += bono.ValorNominal * item.Cantidad;
                    }
                }

                ViewState["TotalCarrito"] = total;

                rptCarrito.DataSource = MiCarrito;
                rptCarrito.DataBind();
            }
        }

        protected void rptCarrito_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                        tdPrecioTasa.InnerText = $"${accion.Precio.ToString("F2")}";
                    }
                    else if (carritoItem.Activo is Bono bono)
                    {
                        tdDetalle.InnerText = $"Tasa: {bono.TasaInteres}%";
                        tdPrecioTasa.InnerText = $"${bono.ValorNominal.ToString("F2")}";
                    }
                }
            }
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangeQuantity")
            {
                var argumentos = e.CommandArgument.ToString().Split(',');
                int carritoId = int.Parse(argumentos[0]);
                int cambioCantidad = int.Parse(argumentos[1]);

                var carritoItem = MiCarrito.Find(c => c.Id == carritoId);
                if (carritoItem != null)
                {
                    carritoItem.Cantidad += cambioCantidad;

                    if (carritoItem.Cantidad < 1)
                    {
                        carritoItem.Cantidad = 1;
                    }

                    _carritoService.ActualizarCarrito(carritoItem);

                    CalcularTotalCarrito();
                    rptCarrito.DataSource = MiCarrito;
                    rptCarrito.DataBind();
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                int carritoId = int.Parse(e.CommandArgument.ToString());

                MiCarrito.RemoveAll(c => c.Id == carritoId);

                _carritoService.EliminarCarrito(carritoId);

                if (MiCarrito.Count == 0)
                {
                    divCarrito.Visible = false;
                    ltlCarritoVacio.Visible = true;
                }
                else
                {
                    CalcularTotalCarrito();
                    rptCarrito.DataSource = MiCarrito;
                    rptCarrito.DataBind();
                }
            }
        }

        private void CalcularTotalCarrito()
        {
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

            ViewState["TotalCarrito"] = total;
        }
    }
}