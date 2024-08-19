﻿using MidMarket.Entities;
using MidMarket.Entities.Enums;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IBitacoraService
    {
        int AltaBitacora(string descripcion, Criticidad criticidad, Cliente cliente);
        List<Bitacora> GetBitacora();
    }
}
