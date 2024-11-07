using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IVentaDAO
    {
        int InsertarTransaccionVenta(TransaccionVenta venta);
        int InsertarDetalleVenta(DetalleVenta venta, int idVenta);
        int ActualizarActivoCliente(Cliente cliente, DetalleVenta detalle);
        List<TransaccionVenta> GetVentas(Cliente cliente);
        decimal ObtenerUltimoPrecioActivo(int idActivo);
        int ObtenerCantidadRealCliente(int idActivo, int idCliente);
        List<TransaccionVentaDTO> GetAllVentas();
        List<DetalleVentaDTO> GetAllVentasDetalle();
    }
}
