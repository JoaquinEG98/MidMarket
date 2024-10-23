using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Enums;
using MidMarket.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class CompraService : ICompraService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IBitacoraService _bitacoraService;
        private readonly ICompraDAO _compraDataAccess;
        private readonly IUsuarioService _usuarioService;

        public CompraService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _compraDataAccess = DependencyResolver.Resolve<ICompraDAO>();
            _usuarioService = DependencyResolver.Resolve<IUsuarioService>();
        }

        public void RealizarCompra(List<Carrito> carrito)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                var saldoActual = _usuarioService.GetCliente(clienteLogueado.Id).Cuenta.Saldo;
                var total = carrito.Sum(x => x.Total);

                ValidarSaldo(saldoActual, total);

                int compraId = _compraDataAccess.InsertarTransaccionCompra(clienteLogueado, total);

                foreach (var item in carrito)
                {
                    _compraDataAccess.InsertarDetalleCompra(item, compraId);
                    _compraDataAccess.InsertarActivoCliente(clienteLogueado, item);
                }

                _usuarioService.ActualizarSaldo(total);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) realizó la Compra con Id: ({total}) por un total de (${total})", Criticidad.Media, clienteLogueado);

                scope.Complete();
            }
        }

        public void ValidarSaldo(decimal saldo, decimal total)
        {
            if (total > saldo)
                throw new System.Exception(Errores.ObtenerError(18));
        }
    }
}
