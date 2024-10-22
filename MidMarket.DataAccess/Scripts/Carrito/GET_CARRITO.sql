SELECT 
    c.Id_Carrito,
    c.Id_Activo,
    c.Cantidad,
	ac.Nombre,
	a.Id_Accion,
    a.Simbolo,
    a.Precio,
	b.Id_Bono,
    b.ValorNominal,
    b.TasaInteres,
    CASE 
        WHEN a.Id_Accion IS NOT NULL THEN 'Accion'
        ELSE 'Bono'
    END AS TipoActivo
FROM Carrito c
INNER JOIN Activo ac ON c.Id_Activo = ac.Id_Activo
LEFT JOIN Accion a ON c.Id_Activo = a.Id_Activo
LEFT JOIN Bono b ON c.Id_Activo = b.Id_Activo
WHERE c.Id_Cliente = {0}