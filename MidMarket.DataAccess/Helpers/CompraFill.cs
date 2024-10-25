using MidMarket.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.Helpers
{
    public static class CompraFill
    {
        public static TransaccionCompra FillObjectTransaccionCompra(DataRow dr, Cliente cliente)
        {
            TransaccionCompra compra = new TransaccionCompra();
            compra.Cliente = new Cliente();
            compra.Cuenta = new Cuenta();

            if (dr.Table.Columns.Contains("Id_Compra") && !Convert.IsDBNull(dr["Id_Compra"]))
                compra.Id = Convert.ToInt32(dr["Id_Compra"]);

            compra.Cuenta = cliente.Cuenta;

            compra.Cliente = cliente;

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
    }
}
