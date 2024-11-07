using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface ICompraService
    {
        void RealizarCompra(List<Carrito> carrito);
        List<TransaccionCompra> GetCompras(bool historico);
        List<TransaccionCompra> GetAllCompras();
        List<DetalleCompraDTO> GetAllComprasDetalle();
        List<ClienteActivoDTO> GetAllClienteActivo();
    }
}
