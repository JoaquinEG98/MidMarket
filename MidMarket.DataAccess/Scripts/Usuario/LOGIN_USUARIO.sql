﻿SELECT TOP 1 cliente.Id_Cliente, cliente.Email, cliente.Password, cliente.Nombre, cliente.CUIT, cliente.Puntaje, cliente.Bloqueo, cliente.DVH, cuenta.Id_Cuenta, cuenta.NumeroCuenta, cuenta.Saldo
FROM Cliente cliente
INNER JOIN Cuenta cuenta on cuenta.Id_Cliente = cliente.Id_Cliente 
WHERE Email = '{0}'