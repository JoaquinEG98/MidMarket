using MidMarket.DataAccess.Conexion;
using MidMarket.DataAccess.Helpers;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
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

        public int InsertarTransaccionVenta(TransaccionVenta venta)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_TRANSACCION_VENTA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cuenta", venta.Cuenta.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", venta.Cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Fecha", venta.Fecha);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Total", venta.Total);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", venta.DVH);

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
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", venta.DVH);

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

        public List<TransaccionVenta> GetAllVentas()
        {
            var ventas = new List<TransaccionVenta>();

            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ALL_VENTAS);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ventas = VentaFill.FillListTransaccionVenta(ds, null);
            }

            return ventas;
        }

        public List<DetalleVenta> GetAllVentasDetalle()
        {
            var detalle = new List<DetalleVenta>();

            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ALL_VENTAS_DETALLE);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                detalle = VentaFill.FillListDetalleVenta(ds);
            }

            return detalle;
        }
    }
}
