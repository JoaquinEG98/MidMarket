using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface ICarritoDAO
    {
        void InsertarCarrito(Activo activo, Cliente cliente);
        List<Carrito> GetCarrito(int clienteId);
        void ActualizarCarrito(Carrito carrito, Cliente cliente);
        void EliminarCarrito(int idCarrito, Cliente cliente);
        void LimpiarCarrito(Cliente cliente);
        List<CarritoDTO> GetCarritoDTO();
    }
}
