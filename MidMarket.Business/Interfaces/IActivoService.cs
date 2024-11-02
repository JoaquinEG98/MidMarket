using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IActivoService
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
