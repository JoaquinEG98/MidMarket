using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface ICarritoDAO
    {
        void InsertarCarrito(Activo activo, Cliente cliente);
        List<Carrito> GetCarrito(int clienteId);
    }
}
