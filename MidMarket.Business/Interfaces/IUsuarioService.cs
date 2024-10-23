﻿using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Cliente cliente);
        void ModificarUsuario(Cliente cliente);
        Cliente Login(string email, string password);
        void Logout();
        List<Cliente> GetClientes();
        Cliente GetCliente(int clienteId);
        List<Cliente> GetClientesEncriptados();
        void CambiarPassword(Cliente cliente, string nuevaPassword, string confirmacionNuevaPassword);
        void ActualizarSaldo(decimal total);
        decimal ObtenerTotalInvertido(decimal total);
    }
}
