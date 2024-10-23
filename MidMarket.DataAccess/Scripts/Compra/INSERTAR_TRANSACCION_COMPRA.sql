INSERT INTO TransaccionCompra (Id_Cuenta, Id_Cliente, Fecha, Total)
OUTPUT inserted.Id_Compra
VALUES (@Id_Cuenta, @Id_Cliente, @Fecha, @Total)