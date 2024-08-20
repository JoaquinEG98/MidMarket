using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface IDigitoVerificadorService
    {
        string ObtenerDVVActual(string tabla);
        List<string> ObtenerDVH(string tabla);
        string CalcularDVV(string tabla);
        void ActualizarDVV(string tabla);
        string ObtenerDVV(string tabla);
        bool ValidarDigitosVerificadores(string tabla);
    }
}
