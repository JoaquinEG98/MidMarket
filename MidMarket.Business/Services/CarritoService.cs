using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Enums;
using System.Collections.Generic;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IBitacoraService _bitacoraService;
        private readonly ICarritoDAO _carritoDataAccess;

        public CarritoService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _carritoDataAccess = DependencyResolver.Resolve<ICarritoDAO>();
        }

        public void InsertarCarrito(Activo activo)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                _carritoDataAccess.InsertarCarrito(activo, clienteLogueado);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) agregó Activo ({activo.Id}) al carrito", Criticidad.Baja, clienteLogueado);

                scope.Complete();
            }
        }

        public List<Carrito> GetCarrito(int idCliente)
        {
            List<Carrito> carrito = _carritoDataAccess.GetCarrito(idCliente);

            return carrito;
        }

        public void ActualizarCarrito(Carrito carrito)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                _carritoDataAccess.ActualizarCarrito(carrito, clienteLogueado);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) actualizó la cantidad del carrito del Activo: ({carrito.Activo.Id}) al carrito", Criticidad.Baja, clienteLogueado);

                scope.Complete();
            }
        }

        public void EliminarCarrito(int carritoId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                _carritoDataAccess.EliminarCarrito(carritoId, clienteLogueado);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) eliminó Activo del carrito", Criticidad.Baja, clienteLogueado);

                scope.Complete();
            }
        }

        public void LimpiarCarrito(int carritoId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                _carritoDataAccess.LimpiarCarrito(clienteLogueado);

                scope.Complete();
            }
        }
    }
}
