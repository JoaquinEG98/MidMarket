SELECT 
    DV.Id_Detalle,
    DV.Id_Activo,
	B.Id_Bono,
	A.Id_Accion,
    AC.Nombre,
    DV.Id_Venta,
    DV.Cantidad,
    CASE 
        WHEN A.Id_Activo IS NOT NULL THEN 'Accion'
        WHEN B.Id_Activo IS NOT NULL THEN 'Bono'
        ELSE 'Desconocido'
    END AS TipoActivo,
    A.Simbolo,
    B.TasaInteres,
    DV.Precio AS PrecioValorNominal,
    DV.Precio * DV.Cantidad AS Total
FROM 
    DetalleVenta DV
LEFT JOIN Activo AC ON DV.Id_Activo = AC.Id_Activo
LEFT JOIN Accion A ON DV.Id_Activo = A.Id_Activo
LEFT JOIN Bono B ON DV.Id_Activo = B.Id_Activo
WHERE 
    DV.Id_Venta = {0}