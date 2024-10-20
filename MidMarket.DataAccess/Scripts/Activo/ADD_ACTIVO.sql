INSERT INTO Activo (Nombre)
OUTPUT inserted.Id_Activo VALUES (@Nombre)