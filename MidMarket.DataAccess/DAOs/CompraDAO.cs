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
    public class CompraDAO : ICompraDAO
    {
        private readonly BBDD _dataAccess;

        public CompraDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public int InsertarDetalleCompra(DetalleCompra detalle, int idCompra)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_DETALLE_COMPRA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", detalle.Activo.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Compra", idCompra);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Precio", detalle.Precio);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", detalle.DVH);

            return _dataAccess.ExecuteNonEscalar();
        }

        public int InsertarTransaccionCompra(TransaccionCompra compra)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_TRANSACCION_COMPRA;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cuenta", compra.Cuenta.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", compra.Cliente.Id);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Fecha", compra.Fecha);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Total", compra.Total);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", compra.DVH);

            return _dataAccess.ExecuteNonEscalar();
        }

        public int InsertarActivoCliente(ClienteActivoDTO clienteActivo)
        {
            _dataAccess.ExecuteCommandText = Scripts.INSERTAR_ACTIVO_CLIENTE;

            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Cliente", clienteActivo.Id_Cliente);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Id_Activo", clienteActivo.Id_Activo);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@Cantidad", clienteActivo.Cantidad);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@DVH", clienteActivo.DVH);

            return _dataAccess.ExecuteNonEscalar();
        }

        public List<TransaccionCompra> GetCompras(Cliente cliente, bool historico)
        {
            var compras = new List<TransaccionCompra>();

            if (historico)
                _dataAccess.SelectCommandText = String.Format(Scripts.GET_COMPRAS, cliente.Id);
            else
                _dataAccess.SelectCommandText = String.Format(Scripts.GET_COMPRAS_ACTIVAS, cliente.Id);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                compras = CompraFill.FillListTransaccionCompra(ds, cliente);

                foreach (var compra in compras)
                {
                    if (historico)
                        _dataAccess.SelectCommandText = String.Format(Scripts.GET_COMPRAS_DETALLE, compra.Id);
                    else
                        _dataAccess.SelectCommandText = String.Format(Scripts.GET_COMPRAS_DETALLE_ACTIVAS, compra.Id);

                    DataSet dsCompra = _dataAccess.ExecuteNonReader();

                    if (dsCompra.Tables[0].Rows.Count > 0)
                    {
                        compra.Detalle = CompraFill.FillListDetalleCompra(dsCompra);
                    }
                }
            }

            return compras;
        }

        public List<TransaccionCompra> GetAllCompras()
        {
            var compras = new List<TransaccionCompra>();

            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ALL_COMPRAS);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                compras = CompraFill.FillListTransaccionCompra(ds, null);
            }

            return compras;
        }

        public List<DetalleCompraDTO> GetAllComprasDetalle()
        {
            var detalle = new List<DetalleCompraDTO>();

            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ALL_COMPRAS_DETALLE);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                detalle = CompraFill.FillListDetalleCompraDTO(ds);
            }

            return detalle;
        }

        public List<ClienteActivoDTO> GetAllClienteActivo()
        {
            var clienteActivo = new List<ClienteActivoDTO>();

            _dataAccess.SelectCommandText = String.Format(Scripts.GET_ALL_ACTIVO_CLIENTE);

            DataSet ds = _dataAccess.ExecuteNonReader();

            if (ds.Tables[0].Rows.Count > 0)
            {
                clienteActivo = CompraFill.FillListClienteActivo(ds);
            }

            return clienteActivo;
        }
    }
}
