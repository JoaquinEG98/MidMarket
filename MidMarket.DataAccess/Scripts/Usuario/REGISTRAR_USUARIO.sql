INSERT INTO Cliente (Email, Password, Nombre, CUIT, Puntaje, DVH)
OUTPUT inserted.Id_Cliente
VALUES (@Email, @Password, @Nombre, @CUIT, 0.0, @DVH);

DECLARE @IdCliente INT;
SET @IdCliente = SCOPE_IDENTITY();

INSERT INTO Cuenta (Id_Cliente, NumeroCuenta, Saldo)
VALUES (@IdCliente, NEXT VALUE FOR dbo.NumeroCuentaSeq, 50000.0);