SELECT 
    DC.Id_Detalle,
    DC.Id_Activo,
    B.Id_Bono,
    A.Id_Accion,
    AC.Nombre,
    DC.Id_Compra,
    DC.Cantidad,
    CASE 
        WHEN A.Id_Activo IS NOT NULL THEN 'Accion'
        WHEN B.Id_Activo IS NOT NULL THEN 'Bono'
        ELSE 'Desconocido'
    END AS TipoActivo,
    A.Simbolo,
    B.TasaInteres,
    COALESCE(A.Precio, B.ValorNominal, 0) AS PrecioValorNominal,
    COALESCE(A.Precio, B.ValorNominal, 0) * DC.Cantidad AS Total
FROM 
    DetalleCompra DC
LEFT JOIN Activo AC ON DC.Id_Activo = AC.Id_Activo
LEFT JOIN Accion A ON DC.Id_Activo = A.Id_Activo
LEFT JOIN Bono B ON DC.Id_Activo = B.Id_Activo
WHERE 
    DC.Id_Compra = {0};
