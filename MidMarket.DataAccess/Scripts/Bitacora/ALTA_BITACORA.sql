INSERT INTO Bitacora (Id_Cliente, Descripcion, Criticidad, Fecha, DVH) 
OUTPUT inserted.Id_Bitacora VALUES (@ClienteId, @Descripcion, @Criticidad, @Fecha, @DVH)