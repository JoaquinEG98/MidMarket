SELECT 
    SUM(CASE 
            WHEN a.Id_Accion IS NOT NULL THEN a.Precio * ca.Cantidad
            WHEN b.Id_Bono IS NOT NULL THEN b.ValorNominal * ca.Cantidad
        END) AS TotalValorizado
FROM Cliente_Activo ca
INNER JOIN Activo act ON ca.Id_Activo = act.Id_Activo
LEFT JOIN Accion a ON act.Id_Activo = a.Id_Activo
LEFT JOIN Bono b ON act.Id_Activo = b.Id_Activo
WHERE ca.Id_Cliente = {0}