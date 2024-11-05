IF EXISTS (SELECT 1 FROM Cliente_Activo WHERE Id_Cliente = @Id_Cliente AND Id_Activo = @Id_Activo)
BEGIN
    UPDATE Cliente_Activo
    SET Cantidad = Cantidad + @Cantidad, DVH = @DVH
    OUTPUT inserted.Id_Cliente_Activo
    WHERE Id_Cliente = @Id_Cliente AND Id_Activo = @Id_Activo;
END
ELSE
BEGIN
    INSERT INTO Cliente_Activo (Id_Cliente, Id_Activo, Cantidad, DVH)
    OUTPUT inserted.Id_Cliente_Activo
    VALUES (@Id_Cliente, @Id_Activo, @Cantidad, @DVH);
END
