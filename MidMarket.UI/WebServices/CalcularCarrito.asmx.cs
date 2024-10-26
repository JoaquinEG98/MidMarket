using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using System.Collections.Generic;
using System.Web.Services;
using Unity;

namespace MidMarket.UI.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CalcularCarrito : WebService
    {
        private readonly ICarritoService _carritoService;

        public CalcularCarrito()
        {
            _carritoService = Global.Container.Resolve<ICarritoService>();
        }

        [WebMethod]
        public decimal CalcularTotalCarrito(List<Entities.Carrito> carrito, string command, int? carritoId = null, int? cambioCantidad = null)
        {
            decimal total = 0;

            if (command == "CambiarCantidad" && carritoId.HasValue && cambioCantidad.HasValue)
            {
                var carritoItem = carrito.Find(c => c.Id == carritoId.Value);
                if (carritoItem != null)
                {
                    carritoItem.Cantidad += cambioCantidad.Value;

                    if (carritoItem.Cantidad < 1)
                    {
                        carritoItem.Cantidad = 1;
                    }

                    if (carritoItem.Activo is Accion accion)
                    {
                        carritoItem.Total = accion.Precio * carritoItem.Cantidad;
                    }
                    else if (carritoItem.Activo is Bono bono)
                    {
                        carritoItem.Total = bono.ValorNominal * carritoItem.Cantidad;
                    }

                    _carritoService.ActualizarCarrito(carritoItem);
                }
            }
            else if (command == "EliminarItem" && carritoId.HasValue)
            {
                carrito.RemoveAll(c => c.Id == carritoId.Value);
                _carritoService.EliminarCarrito(carritoId.Value);
            }

            foreach (var item in carrito)
            {
                total += item.Total;
            }

            return total;
        }
    }
}
