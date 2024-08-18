using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Cliente cliente);
        Cliente Login(string email, string password);
        List<Cliente> GetClientes();
        Cliente GetCliente(int clienteId);
    }
}
