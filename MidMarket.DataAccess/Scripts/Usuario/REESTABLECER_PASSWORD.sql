UPDATE Cliente
SET Password = @Password, Bloqueo = 0
WHERE Email = @Email