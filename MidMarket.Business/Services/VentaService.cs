﻿using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using System.Collections.Generic;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class VentaService : IVentaService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IBitacoraService _bitacoraService;
        private readonly IVentaDAO _ventaDataAccess;
        private readonly IUsuarioService _usuarioService;
        private readonly ITraduccionService _traduccionService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly ICompraService _compraService;

        public VentaService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _ventaDataAccess = DependencyResolver.Resolve<IVentaDAO>();
            _usuarioService = DependencyResolver.Resolve<IUsuarioService>();
            _traduccionService = DependencyResolver.Resolve<ITraduccionService>();
            _digitoVerificadorService = DependencyResolver.Resolve<IDigitoVerificadorService>();
            _compraService = DependencyResolver.Resolve<ICompraService>();
        }

        public void RealizarVenta(DetalleVenta venta)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                ValidarVenta(venta, clienteLogueado.Id);

                var saldoActual = _usuarioService.GetCliente(clienteLogueado.Id).Cuenta.Saldo;
                var total = venta.Cantidad * venta.Precio;

                var transaccionVenta = new TransaccionVenta()
                {
                    Cuenta = clienteLogueado.Cuenta,
                    Cliente = clienteLogueado,
                    Fecha = ClockWrapper.Now(),
                    Total = total
                };
                transaccionVenta.DVH = GenerarDVHTransaccionVenta(transaccionVenta);

                int ventaId = _ventaDataAccess.InsertarTransaccionVenta(transaccionVenta);

                venta.DVH = GenerarDVHDetalleVenta(venta, ventaId);

                _ventaDataAccess.InsertarDetalleVenta(venta, ventaId);

                _ventaDataAccess.ActualizarActivoCliente(clienteLogueado, venta);

                _usuarioService.ActualizarSaldo(total, true);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) realizó la venta con Id: ({ventaId}) por un total de (${total})", Criticidad.Media, clienteLogueado);

                _digitoVerificadorService.ActualizarDVV("DetalleVenta");
                _digitoVerificadorService.ActualizarDVV("TransaccionVenta");
                _digitoVerificadorService.RecalcularDigitosClienteActivo(_compraService);

                scope.Complete();
            }
        }

        private string GenerarDVHDetalleVenta(DetalleVenta venta, int ventaId)
        {
            DetalleVentaDTO detalle = new DetalleVentaDTO()
            {
                Id_Activo = venta.Activo.Id,
                Cantidad = venta.Cantidad,
                Id_Venta = ventaId,
                Precio = venta.Precio,
            };
            detalle.DVH = DigitoVerificador.GenerarDVH(detalle);

            return detalle.DVH;
        }

        private string GenerarDVHTransaccionVenta(TransaccionVenta venta)
        {
            TransaccionVentaDTO ventaDTO = new TransaccionVentaDTO()
            {
                Id_Cliente = venta.Cliente.Id,
                Id_Cuenta = venta.Cuenta.Id,
                Total = venta.Total,
                Fecha = venta.Fecha,
            };
            ventaDTO.DVH = DigitoVerificador.GenerarDVH(ventaDTO);

            return ventaDTO.DVH;
        }

        private void ValidarVenta(DetalleVenta venta, int idCliente)
        {
            int cantidad = ObtenerCantidadRealCliente(venta.Activo.Id, idCliente);

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (venta.Cantidad > cantidad)
                throw new System.Exception($"{_traduccionService.ObtenerMensaje(idioma, "ERR_04")}");
        }

        public List<TransaccionVenta> GetVentas()
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            List<TransaccionVenta> ventas = _ventaDataAccess.GetVentas(clienteLogueado);

            return ventas;
        }

        public decimal ObtenerUltimoPrecioActivo(int idActivo)
        {
            return _ventaDataAccess.ObtenerUltimoPrecioActivo(idActivo);
        }

        private int ObtenerCantidadRealCliente(int activoId, int idCliente)
        {
            return _ventaDataAccess.ObtenerCantidadRealCliente(activoId, idCliente);
        }

        public List<TransaccionVentaDTO> GetAllVentas()
        {
            List<TransaccionVentaDTO> ventas = _ventaDataAccess.GetAllVentas();

            return ventas;
        }

        public List<DetalleVentaDTO> GetAllVentasDetalle()
        {
            List<DetalleVentaDTO> detalle = _ventaDataAccess.GetAllVentasDetalle();

            return detalle;
        }
    }
}
