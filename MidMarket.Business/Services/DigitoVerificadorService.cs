﻿using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;
using System.Transactions;

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
            //_bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
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
            string nuevoDVV = CalcularDVV(tabla);

            _digitoVerificadorDataAccess.ActualizarDVV(tabla, nuevoDVV);
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

        public bool VerificarInconsistenciaTablas()
        {
            bool cliente = ValidarDigitosVerificadores("Cliente");
            bool usuarioPermiso = ValidarDigitosVerificadores("UsuarioPermiso");

            if (!cliente || !usuarioPermiso)
                return false;

            else
                return true;
        }

        private void ActualizarTablaDVH(List<Cliente> clientes)
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

        public void RecalcularTodosDigitosVerificadores(IUsuarioService usuarioService, IPermisoService permisoService)
        {
            var clientes = usuarioService.GetClientesEncriptados();
            ActualizarTablaDVH(clientes);
            ActualizarDVV("Cliente");


            var usuariosPermisos = permisoService.GetUsuariosPermisos();
            ActualizarTablaDVH(usuariosPermisos);
            ActualizarDVV("UsuarioPermiso");
        }

        public void RecalcularDigitosUsuario(IUsuarioService usuarioService, IPermisoService permisoService)
        {
            var clientes = usuarioService.GetClientesEncriptados();
            ActualizarTablaDVH(clientes);
            ActualizarDVV("Cliente");
        }
    }
}
