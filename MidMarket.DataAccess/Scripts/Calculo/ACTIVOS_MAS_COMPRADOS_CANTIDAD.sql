SELECT TOP 5 
	a.Id_Activo,
    a.Nombre AS Activo,
    SUM(dc.Cantidad) AS Total_Cantidad,
    SUM(dc.Cantidad * dc.Precio) AS Monto_Total
FROM 
    TransaccionCompra tc
JOIN 
    DetalleCompra dc ON tc.Id_Compra = dc.Id_Compra
JOIN 
    Activo a ON dc.Id_Activo = a.Id_Activo
WHERE 
    tc.Fecha >= DATEADD(DAY, -30, GETDATE())
GROUP BY 
    a.Id_Activo, a.Nombre
ORDER BY 
    Total_Cantidad DESC;