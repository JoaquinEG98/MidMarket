SELECT DISTINCT 
    AC.Id_Cliente_Activo AS Id_Compra,
    NULL AS Id_Cuenta,
    AC.Id_Cliente, 
    (SELECT MAX(TC.Fecha) 
     FROM TransaccionCompra TC 
     WHERE TC.Id_Cliente = AC.Id_Cliente) AS Fecha,
    SUM(COALESCE(a.Precio * AC.Cantidad, b.ValorNominal * AC.Cantidad, 0)) AS Total
FROM Cliente_Activo AC
LEFT JOIN Accion a ON AC.Id_Activo = a.Id_Activo
LEFT JOIN Bono b ON AC.Id_Activo = b.Id_Activo
WHERE AC.Id_Cliente = {0}
  AND AC.Cantidad > 0
GROUP BY AC.Id_Cliente_Activo, AC.Id_Cliente;
