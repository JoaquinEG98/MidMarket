using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.DAOs;
using MidMarket.DataAccess.Interfaces;
using System.Reflection;
using System;
using System.Transactions;
using MidMarket.Entities;

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
                cliente.Email = EncriptacionService.EncriptarAES(cliente.Email);
                cliente.Password = EncriptacionService.Hash(cliente.Password);
                cliente.RazonSocial = EncriptacionService.EncriptarAES(cliente.RazonSocial);
                cliente.CUIT = EncriptacionService.EncriptarAES(cliente.CUIT);
                cliente.DVH = DigitoVerificadorService.GenerarDVH(cliente);

                int id = _usuarioDataAccess.RegistrarUsuario(cliente);

                scope.Complete();

                return id;
            }
        }
    }
}
