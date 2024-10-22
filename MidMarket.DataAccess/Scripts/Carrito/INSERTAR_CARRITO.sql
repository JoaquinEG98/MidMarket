IF EXISTS (SELECT 1 FROM Carrito WHERE Id_Activo = @Id_Activo AND Id_Cliente = @Id_Cliente)
BEGIN
    UPDATE Carrito
    SET Cantidad = Cantidad + 1
    WHERE Id_Activo = @Id_Activo AND Id_Cliente = @Id_Cliente;
END
ELSE
BEGIN
    INSERT INTO Carrito (Id_Activo, Id_Cliente, Cantidad)
    VALUES (@Id_Activo, @Id_Cliente, 1);
END