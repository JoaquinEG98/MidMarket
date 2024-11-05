INSERT INTO DetalleCompra (Id_Activo, Id_Compra, Cantidad, Precio, DVH)
OUTPUT inserted.Id_Detalle
VALUES (@Id_Activo, @Id_Compra, @Cantidad, @Precio, @DVH)