using MidMarket.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.Helpers
{
    public static class ClienteFill
    {
        public static Cliente FillObjectCliente(DataRow dr)
        {
            Cliente cliente = new Cliente();
            Cuenta cuenta = new Cuenta();

            if (dr.Table.Columns.Contains("Id_Cliente") && !Convert.IsDBNull(dr["Id_Cliente"]))
                cliente.Id = Convert.ToInt32(dr["Id_Cliente"]);

            if (dr.Table.Columns.Contains("Email") && !Convert.IsDBNull(dr["Email"]))
                cliente.Email = Convert.ToString(dr["Email"]);

            if (dr.Table.Columns.Contains("Password") && !Convert.IsDBNull(dr["Password"]))
                cliente.Password = Convert.ToString(dr["Password"]);

            if (dr.Table.Columns.Contains("Nombre") && !Convert.IsDBNull(dr["Nombre"]))
                cliente.RazonSocial = Convert.ToString(dr["Nombre"]);

            if (dr.Table.Columns.Contains("CUIT") && !Convert.IsDBNull(dr["CUIT"]))
                cliente.CUIT = Convert.ToString(dr["CUIT"]);

            if (dr.Table.Columns.Contains("Puntaje") && !Convert.IsDBNull(dr["Puntaje"]))
                cliente.Puntaje = Convert.ToInt32(dr["Puntaje"]);

            if (dr.Table.Columns.Contains("Bloqueo") && !Convert.IsDBNull(dr["Bloqueo"]))
                cliente.Bloqueo = Convert.ToInt32(dr["Bloqueo"]);

            if (dr.Table.Columns.Contains("Id_Cuenta") && !Convert.IsDBNull(dr["Id_Cuenta"]))
                cuenta.Id = Convert.ToInt32(dr["Id_Cuenta"]);

            if (dr.Table.Columns.Contains("NumeroCuenta") && !Convert.IsDBNull(dr["NumeroCuenta"]))
                cuenta.NumeroCuenta = Convert.ToInt32(dr["NumeroCuenta"]);

            if (dr.Table.Columns.Contains("Saldo") && !Convert.IsDBNull(dr["Saldo"]))
                cuenta.Saldo = Convert.ToInt32(dr["Saldo"]);

            cliente.Cuenta = cuenta;

            return cliente;
        }

        public static List<Cliente> FillListCliente(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectCliente(dr)).ToList();
        }
    }
}
