UPDATE Activo
SET Nombre = @Nombre
WHERE Id_Activo = @Id_Activo

UPDATE Accion
SET Simbolo = @Simbolo, Precio = @Precio
WHERE Id_Accion = @Id_Accion