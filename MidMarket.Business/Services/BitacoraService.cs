using MidMarket.Business.Interfaces;
using MidMarket.Business.Seguridad;
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
                    Baja = false
                };
                bitacora.DVH = GenerarDVHBitacora(bitacora);

                id = _bitacoraDAO.AltaBitacora(bitacora);

                _digitoVerificadorService.ActualizarDVV("Bitacora");

                scope.Complete();
            }

            return id;
        }

        private string GenerarDVHBitacora(Bitacora bitacora)
        {
            BitacoraDTO bitacoraDTO = new BitacoraDTO()
            {
                Id_Cliente = bitacora.Cliente.Id,
                Descripcion = bitacora.Descripcion,
                Criticidad = bitacora.Criticidad,
                Fecha = bitacora.Fecha,
                Baja = bitacora.Baja,
            };
            bitacoraDTO.DVH = DigitoVerificador.GenerarDVH(bitacoraDTO);

            return bitacoraDTO.DVH;
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

        public List<Bitacora> LimpiarBitacora()
        {
            List<Bitacora> bitacora = _bitacoraDAO.LimpiarBitacora();

            _digitoVerificadorService.RecalcularDigitosBitacora(this);

            return bitacora;
        }
    }
}
