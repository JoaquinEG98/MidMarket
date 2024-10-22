using MidMarket.Entities;
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

                return itemCarrito;
            }).ToList();
        }
    }
}
