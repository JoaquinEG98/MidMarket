SELECT Id_Venta, Id_Cuenta, Id_Cliente, Fecha, Total
FROM TransaccionVenta
WHERE Id_Cliente = {0}