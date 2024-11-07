﻿using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.Helpers
{
    public static class CompraFill
    {
        public static TransaccionCompra FillObjectTransaccionCompra(DataRow dr, Cliente cliente = null)
        {
            TransaccionCompra compra = new TransaccionCompra();
            compra.Cliente = new Cliente();
            compra.Cuenta = new Cuenta();

            if (dr.Table.Columns.Contains("Id_Compra") && !Convert.IsDBNull(dr["Id_Compra"]))
                compra.Id = Convert.ToInt32(dr["Id_Compra"]);

            if (cliente != null)
            {
                compra.Cuenta = cliente.Cuenta;
                compra.Cliente = cliente;
            }

            if (dr.Table.Columns.Contains("Fecha") && !Convert.IsDBNull(dr["Fecha"]))
                compra.Fecha = Convert.ToDateTime(dr["Fecha"]);

            if (dr.Table.Columns.Contains("Total") && !Convert.IsDBNull(dr["Total"]))
                compra.Total = Convert.ToDecimal(dr["Total"]);

            return compra;
        }

        public static List<TransaccionCompra> FillListTransaccionCompra(DataSet ds, Cliente cliente)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectTransaccionCompra(dr, cliente)).ToList();
        }

        public static DetalleCompra FillObjectDetalleCompra(DataRow dr)
        {
            DetalleCompra detalleCompra = new DetalleCompra();

            if (dr.Table.Columns.Contains("Id_Detalle") && !Convert.IsDBNull(dr["Id_Detalle"]))
                detalleCompra.Id = Convert.ToInt32(dr["Id_Detalle"]);

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
            {
                var idActivo = Convert.ToInt32(dr["Id_Activo"]);

                if (dr.Table.Columns.Contains("TipoActivo") && !Convert.IsDBNull(dr["TipoActivo"]))
                {
                    string tipoActivo = dr["TipoActivo"].ToString();

                    if (tipoActivo == "Accion")
                    {
                        detalleCompra.Activo = new Accion
                        {
                            Id = idActivo,
                            Id_Accion = dr.Table.Columns.Contains("Id_Accion") && !Convert.IsDBNull(dr["Id_Accion"])
                                ? Convert.ToInt32(dr["Id_Accion"])
                                : 0,
                            Simbolo = dr.Table.Columns.Contains("Simbolo") && !Convert.IsDBNull(dr["Simbolo"])
                                ? dr["Simbolo"].ToString()
                                : null,
                            Precio = dr.Table.Columns.Contains("PrecioValorNominal") && !Convert.IsDBNull(dr["PrecioValorNominal"])
                                ? Convert.ToDecimal(dr["PrecioValorNominal"])
                                : 0,
                            Nombre = dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"])
                                ? dr["Nombre"].ToString()
                                : null
                        };
                    }
                    else if (tipoActivo == "Bono")
                    {
                        detalleCompra.Activo = new Bono
                        {
                            Id = idActivo,
                            Id_Bono = dr.Table.Columns.Contains("Id_Bono") && !Convert.IsDBNull(dr["Id_Bono"])
                                ? Convert.ToInt32(dr["Id_Bono"])
                                : 0,
                            ValorNominal = dr.Table.Columns.Contains("PrecioValorNominal") && !Convert.IsDBNull(dr["PrecioValorNominal"])
                                ? Convert.ToDecimal(dr["PrecioValorNominal"])
                                : 0,
                            TasaInteres = dr.Table.Columns.Contains("TasaInteres") && !Convert.IsDBNull(dr["TasaInteres"])
                                ? float.Parse(dr["TasaInteres"].ToString())
                                : 0,
                            Nombre = dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"])
                                ? dr["Nombre"].ToString()
                                : null
                        };
                    }

                    if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                        detalleCompra.Activo.Nombre = Convert.ToString(dr["Nombre"]);
                }
            }

            if (dr.Table.Columns.Contains("Cantidad") && !Convert.IsDBNull(dr["Cantidad"]))
                detalleCompra.Cantidad = Convert.ToInt32(dr["Cantidad"]);

            if (dr.Table.Columns.Contains("Total") && !Convert.IsDBNull(dr["Total"]))
                detalleCompra.Precio = Convert.ToDecimal(dr["Total"]);

            return detalleCompra;
        }

        public static List<DetalleCompra> FillListDetalleCompra(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectDetalleCompra(dr)).ToList();
        }

        public static ClienteActivoDTO FillObjectClienteActivo(DataRow dr)
        {
            ClienteActivoDTO clienteActivo = new ClienteActivoDTO();

            if (dr.Table.Columns.Contains("Id_Cliente_Activo") && !Convert.IsDBNull(dr["Id_Cliente_Activo"]))
                clienteActivo.Id = Convert.ToInt32(dr["Id_Cliente_Activo"]);

            if (dr.Table.Columns.Contains("Id_Cliente") && !Convert.IsDBNull(dr["Id_Cliente"]))
                clienteActivo.Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]);

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
                clienteActivo.Id_Activo = Convert.ToInt32(dr["Id_Activo"]);

            if (dr.Table.Columns.Contains("Cantidad") && !Convert.IsDBNull(dr["Cantidad"]))
                clienteActivo.Cantidad = Convert.ToInt32(dr["Cantidad"]);

            return clienteActivo;
        }

        public static List<ClienteActivoDTO> FillListClienteActivo(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectClienteActivo(dr)).ToList();
        }

        public static DetalleCompraDTO FillObjectDetalleCompraDTO(DataRow dr)
        {
            DetalleCompraDTO detalle = new DetalleCompraDTO();

            if (dr.Table.Columns.Contains("Id_Detalle") && !Convert.IsDBNull(dr["Id_Detalle"]))
                detalle.Id = Convert.ToInt32(dr["Id_Detalle"]);

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
                detalle.Id_Activo = Convert.ToInt32(dr["Id_Activo"]);

            if (dr.Table.Columns.Contains("Id_Compra") && !Convert.IsDBNull(dr["Id_Compra"]))
                detalle.Id_Compra = Convert.ToInt32(dr["Id_Compra"]);

            if (dr.Table.Columns.Contains("Cantidad") && !Convert.IsDBNull(dr["Cantidad"]))
                detalle.Cantidad = Convert.ToInt32(dr["Cantidad"]);

            if (dr.Table.Columns.Contains("Precio") && !Convert.IsDBNull(dr["Precio"]))
                detalle.Precio = Convert.ToDecimal(dr["Precio"]);

            return detalle;
        }

        public static List<DetalleCompraDTO> FillListDetalleCompraDTO(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectDetalleCompraDTO(dr)).ToList();
        }

        public static TransaccionCompraDTO FillObjectTransaccionCompraDTO(DataRow dr)
        {
            TransaccionCompraDTO compra = new TransaccionCompraDTO();

            if (dr.Table.Columns.Contains("Id_Compra") && !Convert.IsDBNull(dr["Id_Compra"]))
                compra.Id = Convert.ToInt32(dr["Id_Compra"]);

            if (dr.Table.Columns.Contains("Id_Cuenta") && !Convert.IsDBNull(dr["Id_Cuenta"]))
                compra.Id_Cuenta = Convert.ToInt64(dr["Id_Cuenta"]);

            if (dr.Table.Columns.Contains("Id_Cliente") && !Convert.IsDBNull(dr["Id_Cliente"]))
                compra.Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]);

            if (dr.Table.Columns.Contains("Fecha") && !Convert.IsDBNull(dr["Fecha"]))
                compra.Fecha = Convert.ToDateTime(dr["Fecha"]);

            if (dr.Table.Columns.Contains("Total") && !Convert.IsDBNull(dr["Total"]))
                compra.Total = Convert.ToDecimal(dr["Total"]);

            return compra;
        }

        public static List<TransaccionCompraDTO> FillListTransaccionCompraDTO(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectTransaccionCompraDTO(dr)).ToList();
        }
    }
}
