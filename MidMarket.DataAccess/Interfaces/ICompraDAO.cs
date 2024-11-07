using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface ICompraDAO
    {
        int InsertarDetalleCompra(DetalleCompra detalle, int idCompra);
        int InsertarTransaccionCompra(TransaccionCompra compra);
        int InsertarActivoCliente(ClienteActivoDTO clienteActivo);
        List<TransaccionCompra> GetCompras(Cliente cliente, bool historico);
        List<TransaccionCompraDTO> GetAllCompras();
        List<DetalleCompraDTO> GetAllComprasDetalle();
        List<ClienteActivoDTO> GetAllClienteActivo();
    }
}
