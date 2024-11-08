SELECT TOP 500 Id_Bitacora, Id_Cliente, Descripcion, Criticidad, Fecha, Baja 
FROM Bitacora 
WHERE Baja = 0
ORDER BY Fecha DESC