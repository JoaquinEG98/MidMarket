﻿using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using System;
using System.Collections.Generic;
using System.Data;
namespace MidMarket.DataAccess.DAOs
{
    public class CarritoDAO : ICarritoDAO
    {
        private readonly BBDD _dataAccess;

        public CarritoDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public void InsertarCarrito(Activo activo, Cliente cliente)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_CARRITO;

            _dataAccess.ExecuteParameters.Parameters.Clear();
            
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", activo.Id);

            _dataAccess.ExecuteNonQuery();
        }

        public List<Carrito> GetCarrito(int clienteId)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_CARRITO, clienteId);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                return CarritoFill.FillListCarrito(ds);
            }

            return new List<Carrito>();
        }
    }
}
