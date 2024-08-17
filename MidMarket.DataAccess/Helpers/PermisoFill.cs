using MidMarket.Entities.Composite;
using MidMarket.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MidMarket.DataAccess.Helpers
{
    public static class PermisoFill
    {
        public static Familia FillObjectFamilia(DataRow dr)
        {
            Familia familia = new Familia();

            if (dr.Table.Columns.Contains("Id_Permiso") && !Convert.IsDBNull(dr["Id_Permiso"]))
                familia.Id = Convert.ToInt32(dr["Id_Permiso"]);


            if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                familia.Nombre = Convert.ToString(dr["Nombre"]);
            return familia;
        }

        public static List<Familia> FillListFamilia(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectFamilia(dr)).ToList();
        }

        public static Patente FillObjectPatente(DataRow dr)
        {
            Patente patente = new Patente();

            if (dr.Table.Columns.Contains("Id_Permiso") && !Convert.IsDBNull(dr["Id_Permiso"]))
                patente.Id = Convert.ToInt32(dr["Id_Permiso"]);


            if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                patente.Nombre = Convert.ToString(dr["Nombre"]);

            if (dr.Table.Columns.Contains("Permiso") && !Convert.IsDBNull(dr["Permiso"]))
                patente.Permiso = (Permiso)Enum.Parse(typeof(Permiso), dr["Permiso"].ToString());

            return patente;
        }

        public static List<Patente> FillListPatente(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectPatente(dr)).ToList();
        }
    }
}
