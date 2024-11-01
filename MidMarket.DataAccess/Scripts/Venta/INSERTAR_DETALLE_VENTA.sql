INSERT INTO DetalleVenta (Id_Activo, Id_Venta, Cantidad, Precio)
OUTPUT inserted.Id_Detalle
VALUES (@Id_Activo, @Id_Venta, @Cantidad, @Precio)