using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IUsuarioDAO
    {
        int RegistrarUsuario(Cliente cliente);
        void ModificarUsuario(Cliente cliente);
        Cliente Login(string email);
        List<Cliente> GetClientes();
        Cliente GetCliente(int clienteId);
        void ActualizarBloqueo(int clienteId);
        void AumentarBloqueo(int clienteId);
        void CambiarPassword(Cliente cliente);
        void ActualizarSaldo(int cuentaId, decimal nuevoSaldo);
        decimal ObtenerTotalInvertido(int clienteId);
    }
}
