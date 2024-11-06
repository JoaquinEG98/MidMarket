using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using MidMarket.Seguridad;
using System.Collections.Generic;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraDAO _bitacoraDAO;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public BitacoraService()
        {
            _bitacoraDAO = DependencyResolver.Resolve<IBitacoraDAO>();
            _digitoVerificadorService = DependencyResolver.Resolve<IDigitoVerificadorService>();
        }

        public int AltaBitacora(string descripcion, Criticidad criticidad, Cliente cliente)
        {
            int id = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                Bitacora bitacora = new Bitacora()
                {
                    Cliente = cliente,
                    Descripcion = descripcion,
                    Criticidad = criticidad,
                    Fecha = ClockWrapper.Now(),
                };

                id = _bitacoraDAO.AltaBitacora(bitacora);

                _digitoVerificadorService.RecalcularDigitosBitacora(this);

                scope.Complete();
            }

            return id;
        }

        public List<Bitacora> ObtenerBitacora()
        {
            List<Bitacora> bitacora = _bitacoraDAO.ObtenerBitacora();

            return bitacora;
        }

        public List<BitacoraDTO> GetAllBitacora()
        {
            List<BitacoraDTO> bitacora = _bitacoraDAO.GetAllBitacora();

            return bitacora;
        }
    }
}
