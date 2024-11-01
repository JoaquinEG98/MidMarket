IF EXISTS (SELECT 1 FROM Cliente_Activo WHERE Id_Cliente = @Id_Cliente AND Id_Activo = @Id_Activo)
BEGIN
    DECLARE @NuevaCantidad INT;
    SET @NuevaCantidad = (SELECT Cantidad FROM Cliente_Activo WHERE Id_Cliente = @Id_Cliente AND Id_Activo = @Id_Activo) - @Cantidad;
    
    IF @NuevaCantidad > 0
    BEGIN
        UPDATE Cliente_Activo
        SET Cantidad = @NuevaCantidad
        OUTPUT inserted.Id_Cliente_Activo
        WHERE Id_Cliente = @Id_Cliente AND Id_Activo = @Id_Activo;
    END
    ELSE
    BEGIN
        DELETE FROM Cliente_Activo
        OUTPUT deleted.Id_Cliente_Activo
        WHERE Id_Cliente = @Id_Cliente AND Id_Activo = @Id_Activo;
    END
END