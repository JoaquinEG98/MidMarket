using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Enums;
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

        public VentaService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _ventaDataAccess = DependencyResolver.Resolve<IVentaDAO>();
            _usuarioService = DependencyResolver.Resolve<IUsuarioService>();
        }

        public void RealizarVenta(DetalleVenta venta)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                var saldoActual = _usuarioService.GetCliente(clienteLogueado.Id).Cuenta.Saldo;
                var total = venta.Cantidad * venta.Precio;

                int ventaId = _ventaDataAccess.InsertarTransaccionVenta(clienteLogueado, total);

                _ventaDataAccess.InsertarDetalleVenta(venta, ventaId);
                _ventaDataAccess.ActualizarActivoCliente(clienteLogueado, venta);

                _usuarioService.ActualizarSaldo(total, true);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) realizó la venta con Id: ({ventaId}) por un total de (${total})", Criticidad.Media, clienteLogueado);

                scope.Complete();
            }
        }

        public List<TransaccionVenta> GetVentas()
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            List<TransaccionVenta> ventas = _ventaDataAccess.GetVentas(clienteLogueado);

            return ventas;
        }
    }
}
