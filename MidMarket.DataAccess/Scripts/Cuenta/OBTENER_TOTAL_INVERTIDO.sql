WITH ActivosComprados AS (
    SELECT 
        AC.Id_Activo,
        act.Nombre,
        AC.Cantidad,
        CASE 
            WHEN a.Precio IS NOT NULL THEN a.Precio
            WHEN b.ValorNominal IS NOT NULL THEN b.ValorNominal
            ELSE 0
        END AS ValorActual
    FROM Cliente_Activo AC
    LEFT JOIN Accion a ON AC.Id_Activo = a.Id_Activo
    LEFT JOIN Bono b ON AC.Id_Activo = b.Id_Activo
    LEFT JOIN Activo act ON AC.Id_Activo = act.Id_Activo
    WHERE AC.Id_Cliente = {0}
      AND AC.Cantidad > 0
)

SELECT SUM(Cantidad * ValorActual) AS TotalInvertido
FROM ActivosComprados;