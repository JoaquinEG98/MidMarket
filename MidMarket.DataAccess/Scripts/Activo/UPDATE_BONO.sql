UPDATE Activo
SET Nombre = @Nombre
WHERE Id_Activo = @Id_Activo

UPDATE Bono
SET ValorNominal = @ValorNominal, TasaInteres = @TasaInteres
WHERE Id_Bono = @Id_Bono