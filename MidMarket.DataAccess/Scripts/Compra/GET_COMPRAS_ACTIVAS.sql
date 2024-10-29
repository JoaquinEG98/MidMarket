SELECT DISTINCT 
    TC.Id_Compra, 
    TC.Id_Cuenta, 
    TC.Id_Cliente, 
    TC.Fecha, 
    SUM(COALESCE(a.Precio * AC.Cantidad, b.ValorNominal * AC.Cantidad, 0)) AS Total
FROM TransaccionCompra TC
JOIN DetalleCompra DC ON TC.Id_Compra = DC.Id_Compra
JOIN Cliente_Activo AC ON AC.Id_Cliente = TC.Id_Cliente AND AC.Id_Activo = DC.Id_Activo
LEFT JOIN Accion a ON AC.Id_Activo = a.Id_Activo
LEFT JOIN Bono b ON AC.Id_Activo = b.Id_Activo
WHERE TC.Id_Cliente = {0}
  AND AC.Cantidad > 0
GROUP BY TC.Id_Compra, TC.Id_Cuenta, TC.Id_Cliente, TC.Fecha;
