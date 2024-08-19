using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using System;
using System.Transactions;
using MidMarket.Entities;
using MidMarket.Business.Seguridad;
using System.Collections.Generic;
using MidMarket.Entities.Enums;

namespace MidMarket.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUsuarioDAO _usuarioDataAccess;
        private readonly IPermisoService _permisoService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IBitacoraService _bitacoraService;

        public UsuarioService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _usuarioDataAccess = DependencyResolver.Resolve<IUsuarioDAO>();
            _permisoService = DependencyResolver.Resolve<IPermisoService>();
            _digitoVerificadorService = DependencyResolver.Resolve<IDigitoVerificadorService>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
        }

        public int RegistrarUsuario(Cliente cliente)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                cliente.Email = Encriptacion.EncriptarAES(cliente.Email);
                cliente.Password = Encriptacion.Hash(cliente.Password);
                cliente.RazonSocial = Encriptacion.EncriptarAES(cliente.RazonSocial);
                cliente.CUIT = Encriptacion.EncriptarAES(cliente.CUIT);
                cliente.DVH = DigitoVerificador.GenerarDVH(cliente);

                int id = _usuarioDataAccess.RegistrarUsuario(cliente);

                _digitoVerificadorService.ActualizarDVV("Cliente");

                scope.Complete();

                return id;
            }
        }

        public Cliente Login(string email, string password)
        {
            string emailEncriptado = Encriptacion.EncriptarAES(email);

            Cliente cliente = _usuarioDataAccess.Login(emailEncriptado);
            if (cliente != null)
            {
                string passwordEncriptada = Encriptacion.Hash(password);

                if (passwordEncriptada == cliente.Password)
                {
                    Cliente clienteDesencriptado = new Cliente()
                    {
                        Id = cliente.Id,
                        Email = Encriptacion.DesencriptarAES(cliente.Email),
                        RazonSocial = Encriptacion.DesencriptarAES(cliente.RazonSocial),
                        CUIT = Encriptacion.DesencriptarAES(cliente.CUIT),
                        Puntaje = cliente.Puntaje,
                        Cuenta = cliente.Cuenta,
                    };
                    _permisoService.GetComponenteUsuario(clienteDesencriptado);

                    _bitacoraService.AltaBitacora("Inició sesión correctamente", Criticidad.Baja, clienteDesencriptado);

                    return clienteDesencriptado;
                }
                else
                {
                    throw new Exception("La contraseña ingresada es incorrecta.");
                }
            }

            return null;
        }

        public void Logout()
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
            _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) cerró sesión", Criticidad.Baja, clienteLogueado);
            
            _sessionManager.Remove("Usuario");
            _sessionManager.AbandonSession();
        }

        public List<Cliente> GetClientes()
        {
            List<Cliente> clientes = _usuarioDataAccess.GetClientes();
            List<Cliente> clientesDesencriptados = new List<Cliente>();

            foreach (Cliente cliente in clientes)
            {
                var clienteDesencriptado = new Cliente()
                {
                    Id = cliente.Id,
                    Email = Encriptacion.DesencriptarAES(cliente.Email),
                    RazonSocial = Encriptacion.DesencriptarAES(cliente.RazonSocial),
                    CUIT = Encriptacion.DesencriptarAES(cliente.CUIT),
                    Puntaje = cliente.Puntaje,
                    Cuenta = cliente.Cuenta,
                };

                clientesDesencriptados.Add(clienteDesencriptado);
            }

            return clientesDesencriptados;
        }

        public Cliente GetCliente(int clienteId)
        {
            Cliente cliente = _usuarioDataAccess.GetCliente(clienteId);

            var clienteDesencriptado = new Cliente()
            {
                Id = cliente.Id,
                Email = Encriptacion.DesencriptarAES(cliente.Email),
                RazonSocial = Encriptacion.DesencriptarAES(cliente.RazonSocial),
                CUIT = Encriptacion.DesencriptarAES(cliente.CUIT),
                Puntaje = cliente.Puntaje,
                Cuenta = cliente.Cuenta,
            };
            _permisoService.GetComponenteUsuario(clienteDesencriptado);

            return clienteDesencriptado;
        }
    }
}
