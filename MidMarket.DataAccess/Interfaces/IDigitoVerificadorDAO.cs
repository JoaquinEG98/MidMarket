using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IDigitoVerificadorDAO
    {
        List<string> ObtenerDVH(string tabla);
        List<string> ObtenerDVHActuales(string tabla);
        int ActualizarDVV(string tabla, string nuevoDVV);
    }
}
