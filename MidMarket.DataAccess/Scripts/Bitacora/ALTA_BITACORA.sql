INSERT INTO Bitacora (Id_Cliente, Descripcion, Criticidad, Fecha) 
OUTPUT inserted.Id_Bitacora VALUES (@ClienteId, @Descripcion, @Criticidad, @Fecha)