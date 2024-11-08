using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IBitacoraDAO
    {
        int AltaBitacora(Bitacora bitacora);
        List<Bitacora> ObtenerBitacora();
        List<BitacoraDTO> GetAllBitacora();
        List<Bitacora> LimpiarBitacora();
    }
}
