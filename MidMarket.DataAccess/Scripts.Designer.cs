﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MidMarket.DataAccess {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Scripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Scripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MidMarket.DataAccess.Scripts", typeof(Scripts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Cuenta
        ///SET Saldo = @Saldo
        ///WHERE Id_Cuenta = @Id_Cuenta.
        /// </summary>
        internal static string ACTUALIZAR_SALDO {
            get {
                return ResourceManager.GetString("ACTUALIZAR_SALDO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Accion (Simbolo, Precio, Id_Activo)
        ///OUTPUT inserted.Id_Accion VALUES (@Simbolo, @Precio, @Id_Activo).
        /// </summary>
        internal static string ADD_ACCION {
            get {
                return ResourceManager.GetString("ADD_ACCION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Activo (Nombre)
        ///OUTPUT inserted.Id_Activo VALUES (@Nombre).
        /// </summary>
        internal static string ADD_ACTIVO {
            get {
                return ResourceManager.GetString("ADD_ACTIVO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Bono (ValorNominal, TasaInteres, Id_Activo)
        ///OUTPUT inserted.Id_Bono VALUES (@ValorNominal, @TasaInteres, @Id_Activo).
        /// </summary>
        internal static string ADD_BONO {
            get {
                return ResourceManager.GetString("ADD_BONO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Bitacora (Id_Cliente, Descripcion, Criticidad, Fecha) 
        ///OUTPUT inserted.Id_Bitacora VALUES (@ClienteId, @Descripcion, @Criticidad, @Fecha).
        /// </summary>
        internal static string ALTA_BITACORA {
            get {
                return ResourceManager.GetString("ALTA_BITACORA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Cliente
        ///SET Bloqueo = Bloqueo + 1
        ///WHERE ID_Cliente = {0}.
        /// </summary>
        internal static string AUMENTAR_BLOQUEO {
            get {
                return ResourceManager.GetString("AUMENTAR_BLOQUEO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM FamiliaPatente WHERE Id_Padre = @PadreId.
        /// </summary>
        internal static string BORRAR_FAMILIA {
            get {
                return ResourceManager.GetString("BORRAR_FAMILIA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM UsuarioPermiso WHERE Id_Cliente = @ClienteId.
        /// </summary>
        internal static string BORRAR_PERMISO_USUARIO {
            get {
                return ResourceManager.GetString("BORRAR_PERMISO_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Cliente
        ///SET Password = @Password
        ///WHERE Id_Cliente = @Id_Cliente.
        /// </summary>
        internal static string CAMBIAR_PASSWORD {
            get {
                return ResourceManager.GetString("CAMBIAR_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 	DELETE FROM Carrito
        ///	WHERE Id_Carrito = @Id_Carrito AND Id_Cliente = @Id_Cliente.
        /// </summary>
        internal static string ELIMINAR_CARRITO {
            get {
                return ResourceManager.GetString("ELIMINAR_CARRITO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT activo.Id_Activo, activo.Nombre, accion.Id_Accion, accion.Simbolo, accion.Precio
        ///FROM Accion accion
        ///INNER JOIN Activo activo
        ///ON activo.id_Activo = accion.Id_Activo.
        /// </summary>
        internal static string GET_ACCIONES {
            get {
                return ResourceManager.GetString("GET_ACCIONES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Id_Bitacora, Id_Cliente, Descripcion, Criticidad, Fecha FROM Bitacora ORDER BY Fecha DESC.
        /// </summary>
        internal static string GET_BITACORA {
            get {
                return ResourceManager.GetString("GET_BITACORA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT activo.Id_Activo, activo.Nombre, bono.Id_Bono, bono.ValorNominal, bono.TasaInteres
        ///FROM Bono bono
        ///INNER JOIN Activo activo
        ///ON activo.id_Activo = bono.Id_Activo.
        /// </summary>
        internal static string GET_BONOS {
            get {
                return ResourceManager.GetString("GET_BONOS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    c.Id_Carrito,
        ///    c.Id_Activo,
        ///    c.Cantidad,
        ///    ac.Nombre,
        ///    a.Id_Accion,
        ///    a.Simbolo,
        ///    a.Precio,
        ///    b.Id_Bono,
        ///    b.ValorNominal,
        ///    b.TasaInteres,
        ///    CASE 
        ///        WHEN a.Id_Accion IS NOT NULL THEN &apos;Accion&apos;
        ///        ELSE &apos;Bono&apos;
        ///    END AS TipoActivo,
        ///    CASE 
        ///        WHEN a.Id_Accion IS NOT NULL THEN a.Precio * c.Cantidad
        ///        ELSE b.ValorNominal * c.Cantidad
        ///    END AS Total
        ///FROM Carrito c
        ///INNER JOIN Activo ac ON c.Id_Activo = ac.Id_Activo
        ///LEFT JOIN Accio [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string GET_CARRITO {
            get {
                return ResourceManager.GetString("GET_CARRITO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Valor FROM DigitoVerificador WHERE Tabla = &apos;{0}&apos;.
        /// </summary>
        internal static string GET_DIGITO_VERTICAL {
            get {
                return ResourceManager.GetString("GET_DIGITO_VERTICAL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM {0}.
        /// </summary>
        internal static string GET_DIGITOS_HORIZONTALES {
            get {
                return ResourceManager.GetString("GET_DIGITOS_HORIZONTALES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WITH RECURSIVO AS (
        ///    SELECT fp.Id_Padre, fp.Id_Hijo 
        ///    FROM FamiliaPatente fp 
        ///    WHERE fp.Id_Padre = {0}
        ///    
        ///    UNION ALL
        ///    
        ///    SELECT fp2.Id_Padre, fp2.Id_Hijo 
        ///    FROM FamiliaPatente fp2 
        ///    INNER JOIN RECURSIVO r ON r.Id_Hijo = fp2.Id_Padre
        ///)
        ///SELECT r.Id_Padre, r.Id_Hijo, p.Id_Permiso, p.Nombre, p.Permiso 
        ///FROM RECURSIVO r 
        ///INNER JOIN Permiso p ON r.Id_Hijo = p.Id_Permiso
        ///GROUP BY r.Id_Padre, r.Id_Hijo, p.Id_Permiso, p.Nombre, p.Permiso;.
        /// </summary>
        internal static string GET_FAMILIA_PATENTE {
            get {
                return ResourceManager.GetString("GET_FAMILIA_PATENTE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT p.Id_Permiso, p.Nombre 
        ///FROM FamiliaPatente fm 
        ///INNER JOIN Permiso p ON p.Id_Permiso = fm.Id_Padre
        ///INNER JOIN Permiso p2 ON p2.Id_Permiso = fm.Id_Hijo 
        ///WHERE fm.Id_Hijo = {0};.
        /// </summary>
        internal static string GET_FAMILIA_VALIDACION {
            get {
                return ResourceManager.GetString("GET_FAMILIA_VALIDACION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Id_Permiso, Nombre, Permiso FROM Permiso WHERE Permiso IS NULL.
        /// </summary>
        internal static string GET_FAMILIAS {
            get {
                return ResourceManager.GetString("GET_FAMILIAS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Id_Permiso, Nombre, Permiso FROM Permiso WHERE Permiso IS NOT NULL.
        /// </summary>
        internal static string GET_PATENTES {
            get {
                return ResourceManager.GetString("GET_PATENTES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Id_Permiso, Nombre, Permiso, Id_Padre, Id_Hijo FROM Permiso p INNER JOIN FamiliaPatente fm on fm.Id_Hijo = p.Id_Permiso WHERE Id_Padre = {0} 
        ///ORDER BY Permiso DESC.
        /// </summary>
        internal static string GET_PATENTES_DE_FAMILIA {
            get {
                return ResourceManager.GetString("GET_PATENTES_DE_FAMILIA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Id_Permiso, Nombre, Permiso FROM Permiso p INNER JOIN UsuarioPermiso up on up.Id_Patente = p.Id_Permiso 
        ///WHERE up.Id_Cliente = {0} ORDER BY Permiso DESC.
        /// </summary>
        internal static string GET_PERMISOS_USUARIO {
            get {
                return ResourceManager.GetString("GET_PERMISOS_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP 1 cliente.Id_Cliente, cliente.Email, cliente.Password, cliente.Nombre, cliente.CUIT, cliente.Puntaje, cliente.DVH, cuenta.Id_Cuenta, cuenta.NumeroCuenta, cuenta.Saldo
        ///FROM Cliente cliente
        ///INNER JOIN Cuenta cuenta on cuenta.Id_Cliente = cliente.Id_Cliente
        ///WHERE cliente.Id_Cliente = {0}.
        /// </summary>
        internal static string GET_USUARIO {
            get {
                return ResourceManager.GetString("GET_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT p.* FROM UsuarioPermiso up INNER JOIN Permiso p on p.Id_Permiso = up.Id_Patente WHERE Id_Cliente = {0}.
        /// </summary>
        internal static string GET_USUARIO_PERMISO {
            get {
                return ResourceManager.GetString("GET_USUARIO_PERMISO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT cliente.Id_Cliente, cliente.Email, cliente.Password, cliente.Nombre, cliente.CUIT, cliente.Puntaje, cliente.DVH, cuenta.Id_Cuenta, cuenta.NumeroCuenta, cuenta.Saldo
        ///FROM Cliente cliente
        ///INNER JOIN Cuenta cuenta on cuenta.Id_Cliente = cliente.Id_Cliente .
        /// </summary>
        internal static string GET_USUARIOS {
            get {
                return ResourceManager.GetString("GET_USUARIOS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT Id_Usuario_Permiso, Id_Cliente, Id_Patente, DVH 
        ///FROM UsuarioPermiso.
        /// </summary>
        internal static string GET_USUARIOS_PERMISOS {
            get {
                return ResourceManager.GetString("GET_USUARIOS_PERMISOS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Permiso (Nombre, Permiso) OUTPUT inserted.Id_Permiso VALUES (@Nombre, @Permiso).
        /// </summary>
        internal static string GUARDAR_COMPONENTE {
            get {
                return ResourceManager.GetString("GUARDAR_COMPONENTE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO FamiliaPatente (Id_Padre, Id_Hijo) VALUES (@PadreId, @HijoId);
        ///UPDATE Permiso SET Nombre = @NombreFamilia WHERE Id_Permiso = @FamiliaId;.
        /// </summary>
        internal static string GUARDAR_FAMILIA {
            get {
                return ResourceManager.GetString("GUARDAR_FAMILIA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO UsuarioPermiso (Id_Cliente, Id_Patente, DVH) VALUES (@ClienteId, @PatenteId, @DVH).
        /// </summary>
        internal static string GUARDAR_PERMISO_USUARIO {
            get {
                return ResourceManager.GetString("GUARDAR_PERMISO_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Cliente_Activo (Id_Cliente, Id_Activo, Cantidad)
        ///OUTPUT inserted.Id_Cliente_Activo
        ///VALUES (@Id_Cliente, @Id_Activo, @Cantidad).
        /// </summary>
        internal static string INSERTAR_ACTIVO_CLIENTE {
            get {
                return ResourceManager.GetString("INSERTAR_ACTIVO_CLIENTE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF EXISTS (SELECT 1 FROM Carrito WHERE Id_Activo = @Id_Activo AND Id_Cliente = @Id_Cliente)
        ///BEGIN
        ///    UPDATE Carrito
        ///    SET Cantidad = Cantidad + 1
        ///    WHERE Id_Activo = @Id_Activo AND Id_Cliente = @Id_Cliente;
        ///END
        ///ELSE
        ///BEGIN
        ///    INSERT INTO Carrito (Id_Activo, Id_Cliente, Cantidad)
        ///    VALUES (@Id_Activo, @Id_Cliente, 1);
        ///END.
        /// </summary>
        internal static string INSERTAR_CARRITO {
            get {
                return ResourceManager.GetString("INSERTAR_CARRITO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO DetalleCompra (Id_Activo, Id_Compra, Cantidad, Precio)
        ///OUTPUT inserted.Id_Detalle
        ///VALUES (@Id_Activo, @Id_Compra, @Cantidad, @Precio).
        /// </summary>
        internal static string INSERTAR_DETALLE_COMPRA {
            get {
                return ResourceManager.GetString("INSERTAR_DETALLE_COMPRA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO TransaccionCompra (Id_Cuenta, Id_Cliente, Fecha, Total)
        ///OUTPUT inserted.Id_Compra
        ///VALUES (@Id_Cuenta, @Id_Cliente, @Fecha, @Total).
        /// </summary>
        internal static string INSERTAR_TRANSACCION_COMPRA {
            get {
                return ResourceManager.GetString("INSERTAR_TRANSACCION_COMPRA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 	DELETE FROM Carrito
        ///	WHERE Id_Cliente = @Id_Cliente.
        /// </summary>
        internal static string LIMPIAR_CARRITO {
            get {
                return ResourceManager.GetString("LIMPIAR_CARRITO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP 1 cliente.Id_Cliente, cliente.Email, cliente.Password, cliente.Nombre, cliente.CUIT, cliente.Puntaje, cliente.Bloqueo, cliente.DVH, cuenta.Id_Cuenta, cuenta.NumeroCuenta, cuenta.Saldo
        ///FROM Cliente cliente
        ///INNER JOIN Cuenta cuenta on cuenta.Id_Cliente = cliente.Id_Cliente 
        ///WHERE Email = &apos;{0}&apos;.
        /// </summary>
        internal static string LOGIN_USUARIO {
            get {
                return ResourceManager.GetString("LOGIN_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Cliente
        ///SET Email = @Email, Nombre = @Nombre, CUIT = @CUIT, DVH = @DVH
        ///WHERE Id_Cliente = @Id_Cliente.
        /// </summary>
        internal static string MODIFICAR_USUARIO {
            get {
                return ResourceManager.GetString("MODIFICAR_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///    SUM(CASE 
        ///            WHEN a.Id_Accion IS NOT NULL THEN a.Precio * ca.Cantidad
        ///            WHEN b.Id_Bono IS NOT NULL THEN b.ValorNominal * ca.Cantidad
        ///        END) AS TotalValorizado
        ///FROM Cliente_Activo ca
        ///INNER JOIN Activo act ON ca.Id_Activo = act.Id_Activo
        ///LEFT JOIN Accion a ON act.Id_Activo = a.Id_Activo
        ///LEFT JOIN Bono b ON act.Id_Activo = b.Id_Activo
        ///WHERE ca.Id_Cliente = {0}.
        /// </summary>
        internal static string OBTENER_TOTAL_INVERTIDO {
            get {
                return ResourceManager.GetString("OBTENER_TOTAL_INVERTIDO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @rutaCompleta NVARCHAR(255) = @RutaBackupParam;
        ///DECLARE @nombreBase NVARCHAR(255) = @NombreBaseParam;
        ///
        ///DECLARE @backupQuery NVARCHAR(MAX) = &apos;BACKUP DATABASE [&apos; + @nombreBase + &apos;] TO DISK = &apos;&apos;&apos; + @rutaCompleta + &apos;&apos;&apos; WITH FORMAT, MEDIANAME = &apos;&apos;SQLServerBackups&apos;&apos;, NAME = &apos;&apos;Full Backup of &apos; + @nombreBase + &apos;&apos;&apos;&apos;;
        ///
        ///EXEC(@backupQuery);.
        /// </summary>
        internal static string REALIZAR_BACKUP {
            get {
                return ResourceManager.GetString("REALIZAR_BACKUP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Cliente (Email, Password, Nombre, CUIT, Puntaje, Bloqueo, DVH)
        ///OUTPUT inserted.Id_Cliente
        ///VALUES (@Email, @Password, @Nombre, @CUIT, 0.0, @Bloqueo, @DVH);
        ///
        ///DECLARE @IdCliente INT;
        ///SET @IdCliente = SCOPE_IDENTITY();
        ///
        ///INSERT INTO Cuenta (Id_Cliente, NumeroCuenta, Saldo)
        ///VALUES (@IdCliente, NEXT VALUE FOR dbo.NumeroCuentaSeq, 50000.0);.
        /// </summary>
        internal static string REGISTRAR_USUARIO {
            get {
                return ResourceManager.GetString("REGISTRAR_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Activo
        ///SET Nombre = @Nombre
        ///WHERE Id_Activo = @Id_Activo
        ///
        ///UPDATE Accion
        ///SET Simbolo = @Simbolo, Precio = @Precio
        ///WHERE Id_Accion = @Id_Accion.
        /// </summary>
        internal static string UPDATE_ACCION {
            get {
                return ResourceManager.GetString("UPDATE_ACCION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Cliente
        ///SET Bloqueo = 0
        ///WHERE ID_Cliente = {0}.
        /// </summary>
        internal static string UPDATE_BLOQUEO {
            get {
                return ResourceManager.GetString("UPDATE_BLOQUEO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Activo
        ///SET Nombre = @Nombre
        ///WHERE Id_Activo = @Id_Activo
        ///
        ///UPDATE Bono
        ///SET ValorNominal = @ValorNominal, TasaInteres = @TasaInteres
        ///WHERE Id_Bono = @Id_Bono.
        /// </summary>
        internal static string UPDATE_BONO {
            get {
                return ResourceManager.GetString("UPDATE_BONO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 	UPDATE Carrito
        ///	SET Cantidad = @Cantidad
        ///	WHERE Id_Activo = @Id_Activo and Id_Cliente = @Id_Cliente.
        /// </summary>
        internal static string UPDATE_CARRITO {
            get {
                return ResourceManager.GetString("UPDATE_CARRITO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE DigitoVerificador SET Valor = @Valor OUTPUT inserted.Id_DigitoVerificador WHERE Tabla = @Tabla.
        /// </summary>
        internal static string UPDATE_DIGITO_VERTICAL {
            get {
                return ResourceManager.GetString("UPDATE_DIGITO_VERTICAL", resourceCulture);
            }
        }
    }
}
