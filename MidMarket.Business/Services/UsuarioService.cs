using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using System;
using System.Transactions;
using MidMarket.Entities;
using MidMarket.Business.Seguridad;
using System.Collections.Generic;
using MidMarket.Entities.Enums;
using System.Text.RegularExpressions;

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
            ValidarUsuario(cliente, cliente.Password);

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
            ValidarUsuario(cliente, password);
            
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
                    _usuarioDataAccess.UpdateBloqueo(cliente.Id);

                    _bitacoraService.AltaBitacora($"{clienteDesencriptado.RazonSocial} ({clienteDesencriptado.Id}) inició sesión correctamente", Criticidad.Baja, clienteDesencriptado);

                    return clienteDesencriptado;
                }
                else
                {
                    _usuarioDataAccess.AumentarBloqueo(cliente.Id);
                    throw new Exception("[ERR-007]: Contraseña incorrecta");
                }
            }

            return null;
        }

        private void ValidarUsuario(Cliente cliente, string password)
        {
            if (string.IsNullOrWhiteSpace(cliente.Password) || string.IsNullOrWhiteSpace(cliente.Email))
                throw new Exception("[ERR-005]: El usuario y/o contraseña no contienen datos");

            if (!ValidarFormatoPassword(password))
                throw new Exception("[ERR-006]: La contraseña no posee el formato correcto");

            if (cliente.Bloqueo >= 3)
                throw new Exception("[ERR-008]: El usuario se encuentra bloqueado. Contáctese con el administrador");
        }

        private bool ValidarFormatoPassword(string password)
        {
            string regex = @"^(?=.*[A-Z])(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, regex);
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
                    Password = cliente.Password,
                    RazonSocial = Encriptacion.DesencriptarAES(cliente.RazonSocial),
                    CUIT = Encriptacion.DesencriptarAES(cliente.CUIT),
                    Puntaje = cliente.Puntaje,
                    Cuenta = cliente.Cuenta,
                };

                clientesDesencriptados.Add(clienteDesencriptado);
            }

            return clientesDesencriptados;
        }

        public List<Cliente> GetClientesEncriptados()
        {
            List<Cliente> clientes = _usuarioDataAccess.GetClientes();
            //List<Cliente> clientesDesencriptados = new List<Cliente>();

            //foreach (Cliente cliente in clientes)
            //{
            //    var clienteDesencriptado = new Cliente()
            //    {
            //        Id = cliente.Id,
            //        Email = Encriptacion.DesencriptarAES(cliente.Email),
            //        Password = cliente.Password,
            //        RazonSocial = Encriptacion.DesencriptarAES(cliente.RazonSocial),
            //        CUIT = Encriptacion.DesencriptarAES(cliente.CUIT),
            //        Puntaje = cliente.Puntaje,
            //        Cuenta = cliente.Cuenta,
            //    };

            //    clientesDesencriptados.Add(clienteDesencriptado);
            //}

            return clientes;
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
