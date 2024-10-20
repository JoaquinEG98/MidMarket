using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IActivoService
    {
        int AltaAccion(Accion accion);
        int AltaBono(Bono bono);
        List<Accion> GetAcciones();
        List<Bono> GetBonos();
    }
}
