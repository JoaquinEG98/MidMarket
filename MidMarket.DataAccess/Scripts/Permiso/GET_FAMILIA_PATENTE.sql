WITH RECURSIVO AS (
    SELECT fp.Id_Padre, fp.Id_Hijo 
    FROM FamiliaPatente fp 
    WHERE fp.Id_Padre = {0}
    
    UNION ALL
    
    SELECT fp2.Id_Padre, fp2.Id_Hijo 
    FROM FamiliaPatente fp2 
    INNER JOIN RECURSIVO r ON r.Id_Hijo = fp2.Id_Padre
)
SELECT r.Id_Padre, r.Id_Hijo, p.Id_Permiso, p.Nombre, p.Permiso 
FROM RECURSIVO r 
INNER JOIN Permiso p ON r.Id_Hijo = p.Id_Permiso
GROUP BY r.Id_Padre, r.Id_Hijo, p.Id_Permiso, p.Nombre, p.Permiso;