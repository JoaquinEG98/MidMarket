INSERT INTO Cliente_Activo (Id_Cliente, Id_Activo, Cantidad)
OUTPUT inserted.Id_Cliente_Activo
VALUES (@Id_Cliente, @Id_Activo, @Cantidad)