using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface ICarritoService
    {
        void InsertarCarrito(Activo activo);
        List<Carrito> GetCarrito(int idCliente);
        void ActualizarCarrito(Carrito carrito);
        void EliminarCarrito(int carritoId);
        void LimpiarCarrito(int carritoId);
        List<CarritoDTO> GetCarritoDTO();
    }
}
