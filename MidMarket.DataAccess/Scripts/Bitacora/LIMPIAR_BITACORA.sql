SELECT TOP 500 Id_Bitacora, Id_Cliente, Descripcion, Criticidad, Fecha
INTO #Ultimos500
FROM Bitacora
WHERE Baja = 0
ORDER BY Id_Bitacora;

UPDATE Bitacora
SET Baja = 1
WHERE Id_Bitacora IN (SELECT Id_Bitacora FROM #Ultimos500);

SELECT * FROM #Ultimos500;

DROP TABLE #Ultimos500;