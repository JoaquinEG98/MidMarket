using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class ActivoService : IActivoService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUsuarioDAO _usuarioDataAccess;
        private readonly IPermisoService _permisoService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;
        private readonly IActivoDAO _activoDataAccess;

        public ActivoService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _usuarioDataAccess = DependencyResolver.Resolve<IUsuarioDAO>();
            _permisoService = DependencyResolver.Resolve<IPermisoService>();
            _digitoVerificadorService = DependencyResolver.Resolve<IDigitoVerificadorService>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _activoDataAccess = DependencyResolver.Resolve<IActivoDAO>();
        }

        public int AltaAccion(Accion accion)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = _activoDataAccess.AltaAccion(accion);

                scope.Complete();

                return id;
            }
        }

        public int AltaBono(Bono bono)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int id = _activoDataAccess.AltaBono(bono);

                scope.Complete();

                return id;
            }
        }
    }
}
