using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface ICompraService
    {
        int RealizarCompra(List<Carrito> carrito);
        List<TransaccionCompra> GetCompras(bool historico);
        List<TransaccionCompraDTO> GetAllCompras();
        List<DetalleCompraDTO> GetAllComprasDetalle();
        List<ClienteActivoDTO> GetAllClienteActivo();
    }
}
