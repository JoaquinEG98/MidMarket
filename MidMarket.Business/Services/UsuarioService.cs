using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.DAOs;
using MidMarket.DataAccess.Interfaces;
using System.Reflection;
using System;
using System.Transactions;
using MidMarket.Entities;
using MidMarket.Business.Seguridad;

namespace MidMarket.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDAO _usuarioDataAccess;

        public UsuarioService()
        {
            _usuarioDataAccess = DependencyResolver.Resolve<IUsuarioDAO>();
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

                scope.Complete();

                return id;
            }
        }
    }
}
