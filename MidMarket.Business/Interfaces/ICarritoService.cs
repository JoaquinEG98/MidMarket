using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface ICarritoService
    {
        void InsertarCarrito(Activo activo);
        List<Carrito> GetCarrito(int idCliente);
    }
}
