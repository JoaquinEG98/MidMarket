﻿using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IVentaService
    {
        void RealizarVenta(DetalleVenta venta);
        List<TransaccionVenta> GetVentas();
        decimal ObtenerUltimoPrecioActivo(int idActivo);
        List<TransaccionVenta> GetAllVentas();
        List<DetalleVentaDTO> GetAllVentasDetalle();
    }
}
