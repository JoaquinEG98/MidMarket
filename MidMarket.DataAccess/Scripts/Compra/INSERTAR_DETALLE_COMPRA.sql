INSERT INTO DetalleCompra (Id_Activo, Id_Compra, Cantidad, Precio)
OUTPUT inserted.Id_Detalle
VALUES (@Id_Activo, @Id_Compra, @Cantidad, @Precio)