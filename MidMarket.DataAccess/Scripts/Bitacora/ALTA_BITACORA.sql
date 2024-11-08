INSERT INTO Bitacora (Id_Cliente, Descripcion, Criticidad, Fecha, Baja, DVH) 
OUTPUT inserted.Id_Bitacora VALUES (@ClienteId, @Descripcion, @Criticidad, @Fecha, 0, @DVH)