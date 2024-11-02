using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IActivoDAO
    {
        int AltaAccion(Accion accion);
        int AltaBono(Bono bono);
        List<Accion> GetAcciones();
        List<Bono> GetBonos();
        void ModificarAccion(Accion accion);
        void ModificarBono(Bono bono);
        List<ActivosCompradosDTO> GetActivosCompradosCantidad();
        List<ActivosCompradosDTO> GetActivosCompradosTotal();
        List<ActivosVendidosDTO> GetActivosVendidosCantidad();
        List<ActivosVendidosDTO> GetActivosVendidosTotal();
    }
}
