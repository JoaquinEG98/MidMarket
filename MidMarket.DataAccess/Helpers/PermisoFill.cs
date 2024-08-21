using MidMarket.Entities.Composite;
using MidMarket.Entities.DTOs;
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

        public static UsuarioPermisoDTO FillObjectUsuarioPermiso(DataRow dr)
        {
            var usuarioPermiso = new UsuarioPermisoDTO();

            if (dr.Table.Columns.Contains("Id_Usuario_Permiso") && !Convert.IsDBNull(dr["Id_Usuario_Permiso"]))
                usuarioPermiso.Id = Convert.ToInt32(dr["Id_Usuario_Permiso"]);

            if (dr.Table.Columns.Contains("Id_Cliente") && !Convert.IsDBNull(dr["Id_Cliente"]))
                usuarioPermiso.UsuarioId = Convert.ToInt32(dr["Id_Cliente"]);

            if (dr.Table.Columns.Contains("Id_Patente") && !Convert.IsDBNull(dr["Id_Patente"]))
                usuarioPermiso.PermisoId = Convert.ToInt32(dr["Id_Patente"]);

            if (dr.Table.Columns.Contains("DVH") && !Convert.IsDBNull(dr["DVH"]))
                usuarioPermiso.DVH = Convert.ToString(dr["DVH"]);


            return usuarioPermiso;
        }

        public static List<UsuarioPermisoDTO> FillListUsuarioPermisoDTO(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectUsuarioPermiso(dr)).ToList();
        }
    }
}
