SELECT 
    AC.Id_Cliente_Activo AS Id_Detalle,
    AC.Id_Activo,
    B.Id_Bono,
    A.Id_Accion,
    Act.Nombre,
    NULL AS Id_Compra,
    AC.Cantidad,
    CASE 
        WHEN A.Id_Activo IS NOT NULL THEN 'Accion'
        WHEN B.Id_Activo IS NOT NULL THEN 'Bono'
        ELSE 'Desconocido'
    END AS TipoActivo,
    A.Simbolo,
    B.TasaInteres,
    COALESCE(A.Precio, B.ValorNominal, 0) AS PrecioValorNominal,
    COALESCE(A.Precio, B.ValorNominal, 0) * AC.Cantidad AS Total
FROM 
    Cliente_Activo AC
LEFT JOIN Activo Act ON AC.Id_Activo = Act.Id_Activo
LEFT JOIN Accion A ON AC.Id_Activo = A.Id_Activo
LEFT JOIN Bono B ON AC.Id_Activo = B.Id_Activo 
WHERE 
    AC.Id_Cliente_Activo = {0};
