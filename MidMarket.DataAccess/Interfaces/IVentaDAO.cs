using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IVentaDAO
    {
        int InsertarTransaccionVenta(Cliente cliente, decimal total);
        int InsertarDetalleVenta(Carrito carrito, int idVenta);
        int ActualizarActivoCliente(Cliente cliente, DetalleVenta detalle);
        List<TransaccionVenta> GetVentas(Cliente cliente);
    }
}
