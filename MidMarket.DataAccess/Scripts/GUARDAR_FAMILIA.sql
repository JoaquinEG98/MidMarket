INSERT INTO FamiliaPatente (Id_Padre, Id_Hijo) VALUES (@PadreId, @HijoId);
UPDATE Permiso SET Nombre = @NombreFamilia WHERE Id_Permiso = @FamiliaId;