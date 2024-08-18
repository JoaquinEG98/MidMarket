SELECT p.Id_Permiso, p.Nombre 
FROM FamiliaPatente fm 
INNER JOIN Permiso p ON p.Id_Permiso = fm.Id_Padre
INNER JOIN Permiso p2 ON p2.Id_Permiso = fm.Id_Hijo 
WHERE fm.Id_Hijo = {0};