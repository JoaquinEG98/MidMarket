using MidMarket.Entities;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IBitacoraDAO
    {
        int AltaBitacora(Bitacora bitacora);
        List<Bitacora> ObtenerBitacora();
    }
}
