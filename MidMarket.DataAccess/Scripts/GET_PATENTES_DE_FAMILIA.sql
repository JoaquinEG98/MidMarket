SELECT Id_Permiso, Nombre, Permiso, Id_Padre, Id_Hijo FROM Permiso p INNER JOIN FamiliaPatente fm on fm.Id_Hijo = p.Id_Permiso WHERE Id_Padre = {0} 
ORDER BY Permiso DESC