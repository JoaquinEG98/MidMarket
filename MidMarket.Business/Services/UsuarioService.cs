using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Enums;
using MidMarket.Seguridad;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Transactions;

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
                cliente.Bloqueo = 0;

                int id = _usuarioDataAccess.RegistrarUsuario(cliente);

                _digitoVerificadorService.ActualizarDVV("Cliente");

                scope.Complete();

                return id;
            }
        }

        public void ModificarUsuario(Cliente cliente)
        {
            ValidarUsuario(cliente, cliente.Password);

            using (TransactionScope scope = new TransactionScope())
            {
                cliente.Email = Encriptacion.EncriptarAES(cliente.Email);
                cliente.Password = Encriptacion.Hash(cliente.Password);
                cliente.RazonSocial = Encriptacion.EncriptarAES(cliente.RazonSocial);
                cliente.CUIT = Encriptacion.EncriptarAES(cliente.CUIT);
                cliente.DVH = DigitoVerificador.GenerarDVH(cliente);

                _usuarioDataAccess.ModificarUsuario(cliente);

                _digitoVerificadorService.RecalcularDigitosUsuario(this, _permisoService);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) modificó el usuario: ({cliente.Id})", Criticidad.Alta, clienteLogueado);

                scope.Complete();
            }
        }

        public Cliente Login(string email, string password)
        {
            string emailEncriptado = Encriptacion.EncriptarAES(email);

            Cliente cliente = _usuarioDataAccess.Login(emailEncriptado);

            try
            {
                if (cliente == null)
                    throw new Exception(Errores.ObtenerError(7));

                ValidarUsuario(cliente, password);

                string passwordEncriptada = Encriptacion.Hash(password);

                if (passwordEncriptada != cliente.Password)
                    throw new Exception(Errores.ObtenerError(7));

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
                _usuarioDataAccess.ActualizarBloqueo(cliente.Id);

                _bitacoraService.AltaBitacora($"{clienteDesencriptado.RazonSocial} ({clienteDesencriptado.Id}) inició sesión correctamente", Criticidad.Baja, clienteDesencriptado);

                return clienteDesencriptado;

            }
            catch (Exception ex)
            {
                if (cliente != null)
                {
                    _usuarioDataAccess.AumentarBloqueo(cliente.Id);
                    _bitacoraService.AltaBitacora($"Intento de login incorrecto para el usuario: {cliente.Id}", Criticidad.Media, cliente);
                }

                throw ex;
            }
        }

        private void ValidarUsuario(Cliente cliente, string password)
        {
            if (cliente is null || string.IsNullOrWhiteSpace(cliente.Password) || string.IsNullOrWhiteSpace(cliente.Email))
                throw new Exception(Errores.ObtenerError(5));

            if (!ValidarFormatoPassword(password))
                throw new Exception(Errores.ObtenerError(6));

            if (cliente.Bloqueo >= 3)
                throw new Exception(Errores.ObtenerError(8));
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
                _permisoService.GetComponenteUsuario(clienteDesencriptado);

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
                Password = cliente.Password,
                RazonSocial = Encriptacion.DesencriptarAES(cliente.RazonSocial),
                CUIT = Encriptacion.DesencriptarAES(cliente.CUIT),
                Puntaje = cliente.Puntaje,
                Cuenta = cliente.Cuenta,
            };
            _permisoService.GetComponenteUsuario(clienteDesencriptado);

            return clienteDesencriptado;
        }

        public void CambiarPassword(Cliente cliente, string nuevaPassword, string confirmacionNuevaPassword)
        {
            cliente.Password = Encriptacion.Hash(cliente.Password);
            ValidarCambioPassword(cliente, nuevaPassword, confirmacionNuevaPassword);

            using (TransactionScope scope = new TransactionScope())
            {
                cliente.Password = Encriptacion.Hash(nuevaPassword);

                _usuarioDataAccess.CambiarPassword(cliente);

                _digitoVerificadorService.RecalcularDigitosUsuario(this, _permisoService);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) cambió su contraseña", Criticidad.Alta, clienteLogueado);

                scope.Complete();
            }
        }

        private void ValidarCambioPassword(Cliente cliente, string nuevaPassword, string confirmacionNuevaPassword)
        {
            var clienteValidacion = GetCliente(cliente.Id);

            if (clienteValidacion.Password != cliente.Password)
                throw new Exception("La contraseña actual no coindice con la proporcionada.");

            if (nuevaPassword != confirmacionNuevaPassword)
                throw new Exception("La confirmación de la nueva contraseña son distintas.");

            if (!ValidarFormatoPassword(nuevaPassword))
                throw new Exception(Errores.ObtenerError(6));
        }

        public void ActualizarSaldo(decimal total)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            decimal nuevoSaldo = clienteLogueado.Cuenta.Saldo - total;

            _usuarioDataAccess.ActualizarSaldo(clienteLogueado.Cuenta.Id, nuevoSaldo);

            var usuarioActualizado = GetCliente(clienteLogueado.Id);
            _sessionManager.Set("Usuario", usuarioActualizado);
        }

        public decimal ObtenerTotalInvertido(decimal total)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            return _usuarioDataAccess.ObtenerTotalInvertido(clienteLogueado.Id);
        }
    }
}
