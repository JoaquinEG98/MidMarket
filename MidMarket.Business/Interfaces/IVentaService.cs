using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IVentaService
    {
        void RealizarVenta(DetalleVenta venta);
        List<TransaccionVenta> GetVentas();
    }
}
