SELECT activo.Id_Activo, activo.Nombre, accion.Id_Accion, accion.Simbolo, accion.Precio
FROM Accion accion
INNER JOIN Activo activo
ON activo.id_Activo = accion.Id_Activo