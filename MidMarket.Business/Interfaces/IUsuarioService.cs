using MidMarket.Entities;

namespace MidMarket.Business.Interfaces
{
    public interface IUsuarioService
    {
        int RegistrarUsuario(Cliente cliente);
        Cliente Login(string email, string password);
    }
}
