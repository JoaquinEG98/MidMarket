﻿using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.Helpers
{
    public static class ActivoFill
    {
        public static Accion FillObjectAccion(DataRow dr)
        {
            Accion accion = new Accion();

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
                accion.Id = Convert.ToInt32(dr["Id_Activo"]);

            if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                accion.Nombre = Convert.ToString(dr["Nombre"]);

            if (dr.Table.Columns.Contains("Id_Accion") && !Convert.IsDBNull(dr["Id_Accion"]))
                accion.Id_Accion = Convert.ToInt32(dr["Id_Accion"]);

            if (dr.Table.Columns.Contains("Simbolo") && !Convert.IsDBNull(dr["Simbolo"]))
                accion.Simbolo = Convert.ToString(dr["Simbolo"]);

            if (dr.Table.Columns.Contains("Precio") && !Convert.IsDBNull(dr["Precio"]))
                accion.Precio = Convert.ToDecimal(dr["Precio"]);

            return accion;
        }

        public static List<Accion> FillListAccion(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectAccion(dr)).ToList();
        }

        public static Bono FillObjectBono(DataRow dr)
        {
            Bono bono = new Bono();

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
                bono.Id = Convert.ToInt32(dr["Id_Activo"]);

            if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                bono.Nombre = Convert.ToString(dr["Nombre"]);

            if (dr.Table.Columns.Contains("Id_Bono") && !Convert.IsDBNull(dr["Id_Bono"]))
                bono.Id_Bono = Convert.ToInt32(dr["Id_Bono"]);

            if (dr.Table.Columns.Contains("ValorNominal") && !Convert.IsDBNull(dr["ValorNominal"]))
                bono.ValorNominal = Convert.ToDecimal(dr["ValorNominal"]);

            if (dr.Table.Columns.Contains("TasaInteres") && !Convert.IsDBNull(dr["TasaInteres"]))
                bono.TasaInteres = float.Parse(dr["TasaInteres"].ToString());

            return bono;
        }

        public static List<Bono> FillListBono(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectBono(dr)).ToList();
        }

        public static ActivosCompradosDTO FillObjectActivosComprados(DataRow dr)
        {
            ActivosCompradosDTO activo = new ActivosCompradosDTO();

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
                activo.Id = Convert.ToInt32(dr["Id_Activo"]);

            if (dr.Table.Columns.Contains("Activo") && !Convert.IsDBNull(dr["Activo"]))
                activo.Nombre = Convert.ToString(dr["Activo"]);

            if (dr.Table.Columns.Contains("Total_Cantidad") && !Convert.IsDBNull(dr["Total_Cantidad"]))
                activo.Cantidad = Convert.ToInt32(dr["Total_Cantidad"]);

            if (dr.Table.Columns.Contains("Monto_Total") && !Convert.IsDBNull(dr["Monto_Total"]))
                activo.Total = Convert.ToDecimal(dr["Monto_Total"]);

            return activo;
        }

        public static List<ActivosCompradosDTO> FillListActivosComprados(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectActivosComprados(dr)).ToList();
        }

        public static ActivosVendidosDTO FillObjectActivosVendidos(DataRow dr)
        {
            ActivosVendidosDTO activo = new ActivosVendidosDTO();

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
                activo.Id = Convert.ToInt32(dr["Id_Activo"]);

            if (dr.Table.Columns.Contains("Activo") && !Convert.IsDBNull(dr["Activo"]))
                activo.Nombre = Convert.ToString(dr["Activo"]);

            if (dr.Table.Columns.Contains("Total_Cantidad") && !Convert.IsDBNull(dr["Total_Cantidad"]))
                activo.Cantidad = Convert.ToInt32(dr["Total_Cantidad"]);

            if (dr.Table.Columns.Contains("Monto_Total") && !Convert.IsDBNull(dr["Monto_Total"]))
                activo.Total = Convert.ToDecimal(dr["Monto_Total"]);

            return activo;
        }

        public static List<ActivosVendidosDTO> FillListActivosVendidos(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectActivosVendidos(dr)).ToList();
        }
    }
}
