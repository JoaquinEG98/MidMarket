using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IDigitoVerificadorDAO
    {
        List<string> ObtenerDVH(string tabla);
        List<string> ObtenerDVHActuales(string tabla);
        int ActualizarDVV(string tabla, string nuevoDVV);
        string ObtenerDVV(string tabla);
        int ActualizarTablaDVH(string tabla, string nuevoDVH, int objetoId);
    }
}
