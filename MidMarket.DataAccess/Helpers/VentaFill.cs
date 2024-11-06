using MidMarket.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.Helpers
{
    public static class VentaFill
    {
        public static TransaccionVenta FillObjectTransaccionVenta(DataRow dr, Cliente cliente = null)
        {
            TransaccionVenta venta = new TransaccionVenta();
            venta.Cliente = new Cliente();
            venta.Cuenta = new Cuenta();

            if (dr.Table.Columns.Contains("Id_Venta") && !Convert.IsDBNull(dr["Id_Venta"]))
                venta.Id = Convert.ToInt32(dr["Id_Venta"]);

            if (cliente != null)
            {
                venta.Cuenta = cliente.Cuenta;
                venta.Cliente = cliente;
            }

            if (dr.Table.Columns.Contains("Fecha") && !Convert.IsDBNull(dr["Fecha"]))
                venta.Fecha = Convert.ToDateTime(dr["Fecha"]);

            if (dr.Table.Columns.Contains("Total") && !Convert.IsDBNull(dr["Total"]))
                venta.Total = Convert.ToDecimal(dr["Total"]);

            return venta;
        }

        public static List<TransaccionVenta> FillListTransaccionVenta(DataSet ds, Cliente cliente)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectTransaccionVenta(dr, cliente)).ToList();
        }

        public static DetalleVenta FillObjectDetalleVenta(DataRow dr)
        {
            DetalleVenta detalleVenta = new DetalleVenta();

            if (dr.Table.Columns.Contains("Id_Detalle") && !Convert.IsDBNull(dr["Id_Detalle"]))
                detalleVenta.Id = Convert.ToInt32(dr["Id_Detalle"]);

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
            {
                var idActivo = Convert.ToInt32(dr["Id_Activo"]);

                if (dr.Table.Columns.Contains("TipoActivo") && !Convert.IsDBNull(dr["TipoActivo"]))
                {
                    string tipoActivo = dr["TipoActivo"].ToString();

                    if (tipoActivo == "Accion")
                    {
                        detalleVenta.Activo = new Accion
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
                        detalleVenta.Activo = new Bono
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
                        detalleVenta.Activo.Nombre = Convert.ToString(dr["Nombre"]);
                }
            }

            if (dr.Table.Columns.Contains("Cantidad") && !Convert.IsDBNull(dr["Cantidad"]))
                detalleVenta.Cantidad = Convert.ToInt32(dr["Cantidad"]);

            if (dr.Table.Columns.Contains("Total") && !Convert.IsDBNull(dr["Total"]))
                detalleVenta.Precio = Convert.ToDecimal(dr["Total"]);

            return detalleVenta;
        }

        public static List<DetalleVenta> FillListDetalleVenta(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectDetalleVenta(dr)).ToList();
        }
    }
}
