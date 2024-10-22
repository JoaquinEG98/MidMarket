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

        // Propiedad para manejar el carrito desde la sesión usando _sessionManager
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

            // Cargar el carrito desde el servicio y almacenarlo en la sesión
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

        protected void rptCarrito_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var carritoItem = (MidMarket.Entities.Carrito)e.Item.DataItem;

                // Encuentra los controles
                var tdDetalle = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdDetalle");
                var tdPrecioTasa = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdPrecioTasa");

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
            }
        }

        // Evento para cambiar la cantidad
        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ChangeQuantity")
            {
                var argumentos = e.CommandArgument.ToString().Split(',');
                int carritoId = int.Parse(argumentos[0]);
                int cambioCantidad = int.Parse(argumentos[1]);

                // Recuperar el carrito desde la sesión
                var carritoItem = MiCarrito.Find(c => c.Id == carritoId);
                if (carritoItem != null)
                {
                    carritoItem.Cantidad += cambioCantidad;

                    if (carritoItem.Cantidad < 1)
                    {
                        carritoItem.Cantidad = 1; // Evita que la cantidad sea menor a 1
                    }

                    // Actualiza la cantidad en la base de datos
                    _carritoService.ActualizarCarrito(carritoItem);

                    // Recalcula el total y vuelve a cargar los datos
                    CalcularTotalCarrito();
                    rptCarrito.DataSource = MiCarrito;
                    rptCarrito.DataBind();
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                // Eliminar ítem del carrito
                int carritoId = int.Parse(e.CommandArgument.ToString());

                // Eliminar el producto del carrito en la memoria (sesión)
                MiCarrito.RemoveAll(c => c.Id == carritoId);

                // Actualiza la base de datos para reflejar el cambio
                _carritoService.EliminarCarrito(carritoId);

                // Recalcular el total y recargar el carrito
                CalcularTotalCarrito();
                rptCarrito.DataSource = MiCarrito;
                rptCarrito.DataBind();
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

            // Actualiza el ViewState y la interfaz
            ViewState["TotalCarrito"] = total;
        }
    }
}
