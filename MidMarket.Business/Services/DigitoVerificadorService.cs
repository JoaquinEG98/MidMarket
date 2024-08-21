using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using System.Collections.Generic;
using MidMarket.Business.Interfaces;
using System.Transactions;
using MidMarket.Entities.Enums;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;

namespace MidMarket.Business.Services
{
    public class DigitoVerificadorService : IDigitoVerificadorService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IDigitoVerificadorDAO _digitoVerificadorDataAccess;
        private readonly IBitacoraService _bitacoraService;

        public DigitoVerificadorService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _digitoVerificadorDataAccess = DependencyResolver.Resolve<IDigitoVerificadorDAO>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
        }

        private string ObtenerDVVActual(string tabla)
        {
            int valor = 0;
            string actualDVV = "";

            List<string> dvhs = _digitoVerificadorDataAccess.ObtenerDVHActuales(tabla);

            if (dvhs.Count > 0)
            {
                foreach (string dvh in dvhs)
                {
                    valor += int.Parse(Encriptacion.DesencriptarAES(dvh));
                }

                actualDVV = Encriptacion.EncriptarAES(valor.ToString());
            }

            return actualDVV;
        }

        private List<string> ObtenerDVH(string tabla)
        {
            List<string> dvhCalculados = _digitoVerificadorDataAccess.ObtenerDVH(tabla);

            return dvhCalculados;
        }

        private string CalcularDVV(string tabla)
        {
            int valor = 0;
            string actualDVV = "";

            List<string> dvhs = ObtenerDVH(tabla);

            if (dvhs.Count > 0)
            {
                foreach (string dvh in dvhs)
                {
                    valor += int.Parse(Encriptacion.DesencriptarAES(dvh));
                }

                actualDVV = Encriptacion.EncriptarAES(valor.ToString());
            }

            return actualDVV;
        }

        public void ActualizarDVV(string tabla)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string nuevoDVV = CalcularDVV(tabla);

                _digitoVerificadorDataAccess.ActualizarDVV(tabla, nuevoDVV);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) recalculó los Digitos Verificadores Verticales de la Tabla: {tabla}", Criticidad.Alta, clienteLogueado);

                scope.Complete();
            }
        }

        private string ObtenerDVV(string tabla)
        {
            string dvv = _digitoVerificadorDataAccess.ObtenerDVV(tabla);
            return dvv;
        }

        public bool ValidarDigitosVerificadores(string tabla)
        {
            string baseDVV = ObtenerDVVActual(tabla);
            string actualDVV = ObtenerDVV(tabla);
            string compararDVV = CalcularDVV(tabla);

            if (baseDVV == actualDVV && actualDVV == compararDVV)
                return true;

            return false;
        }

        private void ActualizarTablaDVH(List<Cliente> clientes)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (Cliente cliente in clientes)
                {
                    Cliente clienteCalculado = new Cliente()
                    {
                        Id = cliente.Id,
                        Email = cliente.Email,
                        Password = cliente.Password,
                        RazonSocial = cliente.RazonSocial,
                        CUIT = cliente.CUIT,
                        Puntaje = cliente.Puntaje,
                    };
                    clienteCalculado.DVH = DigitoVerificador.GenerarDVH(clienteCalculado);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("Cliente", clienteCalculado.DVH, clienteCalculado.Id);
                }
                scope.Complete();
            }
        }

        private void ActualizarTablaDVH(List<UsuarioPermisoDTO> usuariosPermisos)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (UsuarioPermisoDTO usuarioPermiso in usuariosPermisos)
                {
                    var userPermiso = new UsuarioPermisoDTO()
                    {
                        Id = usuarioPermiso.Id,
                        UsuarioId = usuarioPermiso.UsuarioId,
                        PermisoId = usuarioPermiso.PermisoId
                    };
                    userPermiso.DVH = DigitoVerificador.GenerarDVH(userPermiso);

                    _digitoVerificadorDataAccess.ActualizarTablaDVH("UsuarioPermiso", userPermiso.DVH, userPermiso.Id);
                }
                scope.Complete();
            }
        }

        public void RecalcularDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService)
        {
            var clientes = usuarioService.GetClientesEncriptados();
            ActualizarTablaDVH(clientes);
            ActualizarDVV("Cliente");


            var usuariosPermisos = permisoService.GetUsuariosPermisos();
            ActualizarTablaDVH(usuariosPermisos);
            ActualizarDVV("UsuarioPermiso");
        }
    }
}
