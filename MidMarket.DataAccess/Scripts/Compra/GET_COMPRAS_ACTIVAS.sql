SELECT DISTINCT TC.Id_Compra, TC.Id_Cuenta, TC.Id_Cliente, TC.Fecha, TC.Total
FROM TransaccionCompra TC
JOIN DetalleCompra DC ON TC.Id_Compra = DC.Id_Compra
JOIN Cliente_Activo AC ON AC.Id_Cliente = TC.Id_Cliente AND AC.Id_Activo = DC.Id_Activo
WHERE TC.Id_Cliente = {0}
  AND AC.Cantidad > 0;