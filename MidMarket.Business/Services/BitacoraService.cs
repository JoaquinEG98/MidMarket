﻿using System;
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

        public BitacoraService()
        {
            _bitacoraDAO = DependencyResolver.Resolve<IBitacoraDAO>();
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

            return bitacora;
        }
    }
}