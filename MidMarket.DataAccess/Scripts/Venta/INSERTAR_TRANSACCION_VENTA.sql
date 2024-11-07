INSERT INTO TransaccionVenta (Id_Cuenta, Id_Cliente, Fecha, Total, DVH)
OUTPUT inserted.Id_Venta
VALUES (@Id_Cuenta, @Id_Cliente, @Fecha, @Total, @DVH)