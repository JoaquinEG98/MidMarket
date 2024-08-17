SELECT Id_Permiso, Nombre, Permiso FROM Permiso p INNER JOIN UsuarioPermiso up on up.Id_Patente = p.Id_Permiso 
WHERE up.Id_Cliente = {0} ORDER BY Permiso DESC