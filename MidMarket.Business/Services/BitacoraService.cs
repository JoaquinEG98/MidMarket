using System;
using System.Transactions;
using MidMarket.Entities;
using MidMarket.Entities.Enums;
using MidMarket.DataAccess.Interfaces;
using System.Collections.Generic;
using MidMarket.Business.Interfaces;

namespace MidMarket.Business.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraDAO _bitacoraDAO;
        private readonly IUsuarioService _usuarioService;

        public BitacoraService()
        {
            _bitacoraDAO = DependencyResolver.Resolve<IBitacoraDAO>();
            //_usuarioService = DependencyResolver.Resolve<IUsuarioService>();
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
                    Fecha = DateTime.Now,
                };

                id = _bitacoraDAO.AltaBitacora(bitacora);
                scope.Complete();
            }

            return id;
        }

        public List<Bitacora> GetBitacora()
        {
            List<Bitacora> bitacora = _bitacoraDAO.GetBitacora();
            List<Bitacora> bitacoraFull = new List<Bitacora>();

            foreach (var item in bitacora)
            {
                Bitacora bitacoraItem = item;
                //bitacoraItem.Cliente = _usuarioService.GetCliente(item.Cliente.Id);

                bitacoraFull.Add(bitacoraItem);
            }

            return bitacoraFull;
        }
    }
}
