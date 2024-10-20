INSERT INTO Bono (ValorNominal, TasaInteres, Id_Activo)
OUTPUT inserted.Id_Bono VALUES (@ValorNominal, @TasaInteres, @Id_Activo)