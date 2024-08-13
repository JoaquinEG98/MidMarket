﻿using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
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

                    return clienteDesencriptado;
                }
                else
                {
                    throw new Exception("La contraseña ingresada es incorrecta.");
                }
            }

            return null;
        }
    }
}
