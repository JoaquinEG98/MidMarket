using MidMarket.Entities;

namespace MidMarket.DataAccess.Interfaces
{
    public interface ICarritoDAO
    {
        void InsertarCarrito(Activo activo, Cliente cliente);
    }
}
