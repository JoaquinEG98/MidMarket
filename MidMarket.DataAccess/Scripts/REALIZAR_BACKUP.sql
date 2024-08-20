DECLARE @rutaCompleta NVARCHAR(255) = @RutaBackupParam;
DECLARE @nombreBase NVARCHAR(255) = @NombreBaseParam;

DECLARE @backupQuery NVARCHAR(MAX) = 'BACKUP DATABASE [' + @nombreBase + '] TO DISK = ''' + @rutaCompleta + ''' WITH FORMAT, MEDIANAME = ''SQLServerBackups'', NAME = ''Full Backup of ' + @nombreBase + '''';

EXEC(@backupQuery);