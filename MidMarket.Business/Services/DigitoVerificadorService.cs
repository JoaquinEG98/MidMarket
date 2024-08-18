using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using System.Collections.Generic;
using MidMarket.Business.Interfaces;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class DigitoVerificadorService : IDigitoVerificadorService
    {
        private readonly IDigitoVerificadorDAO _digitoVerificadorDataAccess;

        public DigitoVerificadorService()
        {
            _digitoVerificadorDataAccess = DependencyResolver.Resolve<IDigitoVerificadorDAO>();
        }

        public string ObtenerDVVActual(string tabla)
        {
            int valor = 0;
            string actualDVV = "";

            List<string> dvhs = _digitoVerificadorDataAccess.ObtenerDVHActuales(tabla);

            if (dvhs.Count > 0)
            {
                foreach (string dvh in dvhs)
                {
                    valor += int.Parse(Encriptacion.DesencriptarAES(dvh));
                }

                actualDVV = Encriptacion.EncriptarAES(valor.ToString());
            }

            return actualDVV;
        }

        public List<string> ObtenerDVH(string tabla)
        {
            List<string> dvhCalculados = _digitoVerificadorDataAccess.ObtenerDVH(tabla);

            return dvhCalculados;
        }

        public string CalcularDVV(string tabla)
        {
            int valor = 0;
            string actualDVV = "";

            List<string> dvhs = ObtenerDVH(tabla);

            if (dvhs.Count > 0)
            {
                foreach (string dvh in dvhs)
                {
                    valor += int.Parse(Encriptacion.DesencriptarAES(dvh));
                }

                actualDVV = Encriptacion.EncriptarAES(valor.ToString());
            }

            return actualDVV;
        }

        public void ActualizarDVV(string tabla)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                string nuevoDVV = CalcularDVV(tabla);

                _digitoVerificadorDataAccess.ActualizarDVV(tabla, nuevoDVV);

                scope.Complete();
            }
        }
    }
}
