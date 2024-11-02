using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;

namespace MidMarket.DataAccess.DAOs
{
    public class VentaDAO : IVentaDAO
    {
        private readonly BBDD _dataAccess;

        public VentaDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public int InsertarTransaccionVenta(Cliente cliente, decimal total)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_TRANSACCION_VENTA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cuenta", cliente.Cuenta.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Fecha", ClockWrapper.Now());
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Total", total);

            return _dataAccess.ExecuteNonEscalar();
        }

        public int InsertarDetalleVenta(DetalleVenta venta, int idVenta)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_DETALLE_VENTA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", venta.Activo.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Venta", idVenta);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Precio", venta.Precio);

            return _dataAccess.ExecuteNonEscalar();
        }

        public int ActualizarActivoCliente(Cliente cliente, DetalleVenta detalle)
        {
            _dataAccess.ExecuteCommandText = Scripts.ACTUALIZAR_ACTIVO_CLIENTE;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", detalle.Activo.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);

            return _dataAccess.ExecuteNonEscalar();
        }

        public List<TransaccionVenta> GetVentas(Cliente cliente)
        {
            var ventas = new List<TransaccionVenta>();

            _dataAccess.SelectCommandText = String.Format(Scripts.GET_VENTAS, cliente.Id);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ventas = VentaFill.FillListTransaccionVenta(ds, cliente);

                foreach (var venta in ventas)
                {
                    _dataAccess.SelectCommandText = String.Format(Scripts.GET_VENTAS_DETALLE, venta.Id);

                    DataSet dsCompra = _dataAccess.ExecuteNonReader();

                    if (dsCompra.Tables[0].Rows.Count > 0)
                    {
                        venta.Detalle = VentaFill.FillListDetalleVenta(dsCompra);
                    }
                }
            }

            return ventas;
        }

        public decimal ObtenerUltimoPrecioActivo(int idActivo)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.OBTENER_PRECIO_ACTUAL, idActivo);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var valor = ds.Tables[0].Rows[0][0];

                if (valor != DBNull.Value)
                {
                    return Convert.ToDecimal(valor);
                }
            }

            return 0;
        }

        public int ObtenerCantidadRealCliente(int idActivo, int idCliente)
        {
            _dataAccess.SelectCommandText = String.Format(Scripts.OBTENER_CANTIDAD_REAL_CLIENTE, idActivo, idCliente);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var valor = ds.Tables[0].Rows[0][0];

                if (valor != DBNull.Value)
                {
                    return Convert.ToInt32(valor);
                }
            }

            return 0;
        }
    }
}
