﻿SELECT p.* FROM UsuarioPermiso up INNER JOIN Permiso p on p.Id_Permiso = up.Id_Patente WHERE Id_Cliente = {0}