using MidMarket.Entities;

namespace MidMarket.DataAccess.Interfaces
{
    public interface ICompraDAO
    {
        int InsertarDetalleCompra(Carrito carrito, int idCompra);
        int InsertarTransaccionCompra(Cliente cliente, decimal total);
        int InsertarActivoCliente(Cliente cliente, Carrito carrito);
    }
}
