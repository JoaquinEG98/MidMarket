SELECT activo.Id_Activo, activo.Nombre, bono.Id_Bono, bono.ValorNominal, bono.TasaInteres
FROM Bono bono
INNER JOIN Activo activo
ON activo.id_Activo = bono.Id_Activo