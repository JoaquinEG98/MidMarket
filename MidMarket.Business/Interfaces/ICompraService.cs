using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface ICompraService
    {
        void RealizarCompra(List<Carrito> carrito);
        List<TransaccionCompra> GetCompras();
    }
}
