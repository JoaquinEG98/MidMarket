SELECT TOP 1 Total as UltimaTransaccion
FROM TransaccionCompra
WHERE Id_Cliente = {0}
ORDER BY Fecha DESC