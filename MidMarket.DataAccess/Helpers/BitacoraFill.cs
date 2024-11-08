using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.Helpers
{
    public static class BitacoraFill
    {
        public static Bitacora FillObjectBitacora(DataRow dr)
        {
            Bitacora bitacora = new Bitacora();
            bitacora.Cliente = new Cliente();

            if (dr.Table.Columns.Contains("Id_Bitacora") && !Convert.IsDBNull(dr["Id_Bitacora"]))
                bitacora.Id = Convert.ToInt32(dr["Id_Bitacora"]);

            if (dr.Table.Columns.Contains("Id_Cliente") && !Convert.IsDBNull(dr["Id_Cliente"]))
                bitacora.Cliente.Id = Convert.ToInt32(dr["Id_Cliente"]);

            if (dr.Table.Columns.Contains("Descripcion") && !Convert.IsDBNull(dr["Descripcion"]))
                bitacora.Descripcion = Convert.ToString(dr["Descripcion"]);

            if (dr.Table.Columns.Contains("Criticidad") && !Convert.IsDBNull(dr["Criticidad"]))
                bitacora.Criticidad = (Criticidad)(dr["Criticidad"]);

            if (dr.Table.Columns.Contains("Fecha") && !Convert.IsDBNull(dr["Fecha"]))
                bitacora.Fecha = Convert.ToDateTime(dr["Fecha"]);

            if (dr.Table.Columns.Contains("Baja") && !Convert.IsDBNull(dr["Baja"]))
                bitacora.Baja = Convert.ToBoolean(dr["Baja"]);

            return bitacora;
        }

        public static List<Bitacora> FillListBitacora(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectBitacora(dr)).ToList();
        }

        public static BitacoraDTO FillObjectBitacoraDTO(DataRow dr)
        {
            BitacoraDTO bitacora = new BitacoraDTO();

            if (dr.Table.Columns.Contains("Id_Bitacora") && !Convert.IsDBNull(dr["Id_Bitacora"]))
                bitacora.Id = Convert.ToInt32(dr["Id_Bitacora"]);

            if (dr.Table.Columns.Contains("Id_Cliente") && !Convert.IsDBNull(dr["Id_Cliente"]))
                bitacora.Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]);

            if (dr.Table.Columns.Contains("Descripcion") && !Convert.IsDBNull(dr["Descripcion"]))
                bitacora.Descripcion = Convert.ToString(dr["Descripcion"]);

            if (dr.Table.Columns.Contains("Criticidad") && !Convert.IsDBNull(dr["Criticidad"]))
                bitacora.Criticidad = (Criticidad)(dr["Criticidad"]);

            if (dr.Table.Columns.Contains("Fecha") && !Convert.IsDBNull(dr["Fecha"]))
                bitacora.Fecha = Convert.ToDateTime(dr["Fecha"]);

            if (dr.Table.Columns.Contains("Baja") && !Convert.IsDBNull(dr["Baja"]))
                bitacora.Baja = Convert.ToBoolean(dr["Baja"]);

            return bitacora;
        }

        public static List<BitacoraDTO> FillListBitacoraDTO(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectBitacoraDTO(dr)).ToList();
        }
    }
}
