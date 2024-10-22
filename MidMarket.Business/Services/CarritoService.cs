using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities.Enums;
using MidMarket.Entities;
using System.Transactions;
using System.Collections.Generic;

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
    }
}
