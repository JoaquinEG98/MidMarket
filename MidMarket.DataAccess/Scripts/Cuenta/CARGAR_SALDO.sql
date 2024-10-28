UPDATE Cuenta
SET Saldo = Saldo + @Saldo
WHERE Id_Cliente = @Id_Cliente AND Id_Cuenta = @Id_Cuenta