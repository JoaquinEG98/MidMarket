SELECT Id_Compra, Id_Cuenta, Id_Cliente, Fecha, Total
FROM TransaccionCompra
WHERE Id_Cliente = {0}