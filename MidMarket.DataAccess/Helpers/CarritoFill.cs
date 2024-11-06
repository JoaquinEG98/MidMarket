using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MidMarket.DataAccess.Helpers
{
    public static class CarritoFill
    {
        public static List<Carrito> FillListCarrito(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr =>
            {
                Carrito itemCarrito = new Carrito();
                itemCarrito.Id = Convert.ToInt32(dr["Id_Carrito"]);
                itemCarrito.Nombre = Convert.ToString(dr["Nombre"]);

                string tipoActivo = Convert.ToString(dr["TipoActivo"]);
                if (tipoActivo == "Accion")
                {
                    itemCarrito.Activo = ActivoFill.FillObjectAccion(dr);
                }
                else if (tipoActivo == "Bono")
                {
                    itemCarrito.Activo = ActivoFill.FillObjectBono(dr);
                }

                if (dr.Table.Columns.Contains("Cantidad") && !Convert.IsDBNull(dr["Cantidad"]))
                    itemCarrito.Cantidad = Convert.ToInt32(dr["Cantidad"]);

                if (dr.Table.Columns.Contains("Total") && !Convert.IsDBNull(dr["Total"]))
                    itemCarrito.Total = Convert.ToInt32(dr["Total"]);

                return itemCarrito;
            }).ToList();
        }

        public static CarritoDTO FillObjectCarritoDTO(DataRow dr)
        {
            CarritoDTO carrito = new CarritoDTO();

            if (dr.Table.Columns.Contains("Id_Carrito") && !Convert.IsDBNull(dr["Id_Carrito"]))
                carrito.Id = Convert.ToInt32(dr["Id_Carrito"]);

            if (dr.Table.Columns.Contains("Id_Activo") && !Convert.IsDBNull(dr["Id_Activo"]))
                carrito.Id_Activo = Convert.ToInt32(dr["Id_Activo"]);

            if (dr.Table.Columns.Contains("Id_Cliente") && !Convert.IsDBNull(dr["Id_Cliente"]))
                carrito.Id_Cliente = Convert.ToInt32(dr["Id_Cliente"]);

            if (dr.Table.Columns.Contains("Cantidad") && !Convert.IsDBNull(dr["Cantidad"]))
                carrito.Cantidad = Convert.ToInt32(dr["Cantidad"]);

            if (dr.Table.Columns.Contains("DVH") && !Convert.IsDBNull(dr["DVH"]))
                carrito.DVH = Convert.ToString(dr["DVH"]);

            return carrito;
        }


        public static List<CarritoDTO> FillListCarritoDTO(DataSet ds)
        {
            return ds.Tables[0].AsEnumerable().Select(dr => FillObjectCarritoDTO(dr)).ToList();
        }
    }
}
