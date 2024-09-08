﻿using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IUsuarioDAO
    {
        int RegistrarUsuario(Cliente cliente);
        Cliente Login(string email);
        List<Cliente> GetClientes();
        Cliente GetCliente(int clienteId);
        void UpdateBloqueo(int clienteId);
        void AumentarBloqueo(int clienteId);
    }
}
