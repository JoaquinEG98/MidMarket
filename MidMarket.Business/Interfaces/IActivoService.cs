using MidMarket.Entities;

namespace MidMarket.Business.Interfaces
{
    public interface IActivoService
    {
        int AltaAccion(Accion accion);
        int AltaBono(Bono bono);
    }
}
