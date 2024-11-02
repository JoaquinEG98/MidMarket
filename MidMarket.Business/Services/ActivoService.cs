using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using System.Collections.Generic;
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
                ValidarAccion(accion);

                int id = _activoDataAccess.AltaAccion(accion);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) dio de alta la Acción ({id} - {accion.Nombre})", Criticidad.Media, clienteLogueado);

                scope.Complete();

                return id;
            }
        }

        public int AltaBono(Bono bono)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ValidarBono(bono);

                int id = _activoDataAccess.AltaBono(bono);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) dio de alta el Bono ({id} - {bono.Nombre})", Criticidad.Media, clienteLogueado);

                scope.Complete();

                return id;
            }
        }

        public List<Accion> GetAcciones()
        {
            List<Accion> acciones = _activoDataAccess.GetAcciones();

            return acciones;
        }

        public List<Bono> GetBonos()
        {
            List<Bono> bonos = _activoDataAccess.GetBonos();

            return bonos;
        }

        public void ModificarAccion(Accion accion)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ValidarAccion(accion);

                _activoDataAccess.ModificarAccion(accion);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) modificó la Accion ({accion.Id_Accion} - {accion.Nombre})", Criticidad.Media, clienteLogueado);

                scope.Complete();
            }
        }

        public void ModificarBono(Bono bono)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ValidarBono(bono);

                _activoDataAccess.ModificarBono(bono);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) modificó el Bono ({bono.Id_Bono} - {bono.Nombre})", Criticidad.Media, clienteLogueado);

                scope.Complete();
            }
        }

        private void ValidarAccion(Accion accion)
        {
            if (string.IsNullOrWhiteSpace(accion.Nombre) || accion.Precio <= 0 || string.IsNullOrWhiteSpace(accion.Simbolo))
                throw new System.Exception("ERR-002 campos obligatorios incompletos");
        }

        private void ValidarBono(Bono bono)
        {
            if (string.IsNullOrWhiteSpace(bono.Nombre) || bono.ValorNominal <= 0 || bono.TasaInteres <= 0)
                throw new System.Exception("ERR-002 campos obligatorios incompletos");
        }

        public List<ActivosCompradosDTO> GetActivosCompradosCantidad()
        {
            List<ActivosCompradosDTO> activos = _activoDataAccess.GetActivosCompradosCantidad();

            return activos;
        }

        public List<ActivosCompradosDTO> GetActivosCompradosTotal()
        {
            List<ActivosCompradosDTO> activos = _activoDataAccess.GetActivosCompradosTotal();

            return activos;
        }

        public List<ActivosVendidosDTO> GetActivosVendidosCantidad()
        {
            List<ActivosVendidosDTO> activos = _activoDataAccess.GetActivosVendidosCantidad();

            return activos;
        }

        public List<ActivosVendidosDTO> GetActivosVendidosTotal()
        {
            List<ActivosVendidosDTO> activos = _activoDataAccess.GetActivosVendidosTotal();

            return activos;
        }
    }
}
