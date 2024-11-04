using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Enums;
using MidMarket.Entities.Observer;
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
        private readonly ITraduccionService _traduccionService;

        public CompraService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _compraDataAccess = DependencyResolver.Resolve<ICompraDAO>();
            _usuarioService = DependencyResolver.Resolve<IUsuarioService>();
            _traduccionService = DependencyResolver.Resolve<ITraduccionService>();
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

                _usuarioService.ActualizarSaldo(total, false);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) realizó la Compra con Id: ({compraId}) por un total de (${total})", Criticidad.Media, clienteLogueado);

                scope.Complete();
            }
        }

        public void ValidarSaldo(decimal saldo, decimal total)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (total > saldo)
                throw new System.Exception($"{_traduccionService.ObtenerMensaje(idioma, "ERR_18")}");
        }

        public List<TransaccionCompra> GetCompras(bool historico)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            List<TransaccionCompra> compras = _compraDataAccess.GetCompras(clienteLogueado, historico);

            return compras;
        }
    }
}
