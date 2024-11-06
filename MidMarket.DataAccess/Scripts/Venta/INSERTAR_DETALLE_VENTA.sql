INSERT INTO DetalleVenta (Id_Activo, Id_Venta, Cantidad, Precio, DVH)
OUTPUT inserted.Id_Detalle
VALUES (@Id_Activo, @Id_Venta, @Cantidad, @Precio, @DVH)