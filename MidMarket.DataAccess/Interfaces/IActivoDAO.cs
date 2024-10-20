using MidMarket.Entities;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IActivoDAO
    {
        int AltaAccion(Accion accion);
        int AltaBono(Bono bono);
    }
}
