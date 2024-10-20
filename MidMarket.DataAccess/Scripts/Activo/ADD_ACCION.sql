INSERT INTO Accion (Simbolo, Precio, Id_Activo)
OUTPUT inserted.Id_Accion VALUES (@Simbolo, @Precio, @Id_Activo)