using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface ICompraDAO
    {
        int InsertarDetalleCompra(Carrito carrito, int idCompra);
        int InsertarTransaccionCompra(Cliente cliente, decimal total);
        int InsertarActivoCliente(Cliente cliente, Carrito carrito);
        List<TransaccionCompra> GetCompras(Cliente cliente);
    }
}
