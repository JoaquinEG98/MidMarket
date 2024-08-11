using MidMarket.Entities;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IUsuarioDAO
    {
        int RegistrarUsuario(Cliente cliente);
    }
}
