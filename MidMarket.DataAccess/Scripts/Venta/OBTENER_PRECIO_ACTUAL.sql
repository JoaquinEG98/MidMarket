SELECT 
    CASE 
        WHEN a.Id_Activo IN (SELECT Id_Activo FROM accion) THEN ac.Precio
        WHEN a.Id_Activo IN (SELECT Id_Activo FROM bono) THEN b.ValorNominal
    END AS Precio
FROM activo a
LEFT JOIN accion ac ON a.Id_Activo = ac.Id_Activo
LEFT JOIN bono b ON a.Id_Activo = b.Id_Activo
WHERE a.Id_Activo = {0}