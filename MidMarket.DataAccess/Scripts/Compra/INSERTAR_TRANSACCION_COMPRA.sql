INSERT INTO TransaccionCompra (Id_Cuenta, Id_Cliente, Fecha, Total, DVH)
OUTPUT inserted.Id_Compra
VALUES (@Id_Cuenta, @Id_Cliente, @Fecha, @Total, @DVH)