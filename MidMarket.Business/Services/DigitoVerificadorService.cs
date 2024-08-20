using MidMarket.Business.Seguridad;
using MidMarket.DataAccess.Interfaces;
using System.Collections.Generic;
using MidMarket.Business.Interfaces;
using System.Transactions;
using MidMarket.Entities.Enums;
using MidMarket.Entities;

namespace MidMarket.Business.Services
{
    public class DigitoVerificadorService : IDigitoVerificadorService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IDigitoVerificadorDAO _digitoVerificadorDataAccess;
        private readonly IBitacoraService _bitacoraService;

        public DigitoVerificadorService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _digitoVerificadorDataAccess = DependencyResolver.Resolve<IDigitoVerificadorDAO>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
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

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) recalculó los Digitos Verificadores Verticales de la Tabla: {tabla}", Criticidad.Alta, clienteLogueado);
                
                scope.Complete();
            }
        }

        public string ObtenerDVV(string tabla)
        {
            string dvv = _digitoVerificadorDataAccess.ObtenerDVV(tabla);
            return dvv;
        }

        public bool ValidarDigitosVerificadores(string tabla)
        {
            string actualDVV = ObtenerDVV(tabla);
            string compararDVV = CalcularDVV(tabla);

            if (actualDVV != compararDVV)
                return false;

            return true;
        }
    }
}
