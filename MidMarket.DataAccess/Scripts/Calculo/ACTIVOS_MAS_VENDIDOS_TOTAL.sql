	SELECT TOP 5
	a.Id_Activo,
    a.Nombre AS Activo,
    SUM(dc.Cantidad) AS Total_Cantidad,
    SUM(dc.Cantidad * dc.Precio) AS Monto_Total
FROM 
    TransaccionVenta tv
JOIN 
    DetalleVenta dc ON tv.Id_Venta = dc.Id_Venta
JOIN 
    Activo a ON dc.Id_Activo = a.Id_Activo
WHERE 
    tv.Fecha >= DATEADD(DAY, -30, GETDATE())
GROUP BY 
    a.Id_Activo, a.Nombre
ORDER BY 
    Monto_Total DESC;