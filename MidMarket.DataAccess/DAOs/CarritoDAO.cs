using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
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

        public void ActualizarCarrito(Carrito carrito, Cliente cliente)
        {
            _dataAccess.ExecuteCommandText = Scripts.UPDATE_CARRITO;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", carrito.Activo.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Cantidad", carrito.Cantidad);

            _dataAccess.ExecuteNonQuery();
        }

        public void EliminarCarrito(int idCarrito, Cliente cliente)
        {
            _dataAccess.ExecuteCommandText = Scripts.ELIMINAR_CARRITO;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Carrito", idCarrito);

            _dataAccess.ExecuteNonQuery();
        }

        public void LimpiarCarrito(Cliente cliente)
        {
            _dataAccess.ExecuteCommandText = Scripts.LIMPIAR_CARRITO;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);

            _dataAccess.ExecuteNonQuery();
        }

        public List<CarritoDTO> GetCarritoDTO()
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ALL_CARRITO);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                return CarritoFill.FillListCarritoDTO(ds);
            }

            return new List<CarritoDTO>();
        }
    }
}
