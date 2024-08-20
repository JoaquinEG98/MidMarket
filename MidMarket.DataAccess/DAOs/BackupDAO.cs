using MidMarket.DataAccess.Conexion;
using MidMarket.Entities.DTOs;
using MidMarket.DataAccess.Interfaces;
using System;

namespace MidMarket.DataAccess.DAOs
{
    public class BackupDAO : IBackupDAO
    {
        private readonly BBDD _dataAccess;

        public BackupDAO()
        {
            _dataAccess = BBDD.GetInstance;
        }

        public bool RealizarBackup(BackupDTO backup)
        {
            bool backupRealizado = false;

            _dataAccess.ExecuteCommandText = Scripts.REALIZAR_BACKUP;
            _dataAccess.ExecuteParameters.Parameters.Clear();

            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@RutaBackupParam", backup.RutaBackup);
            _dataAccess.ExecuteParameters.Parameters.AddWithValue("@NombreBaseParam", backup.NombreBase);

            _dataAccess.ExecuteNonQueryNonTransaction();

            backupRealizado = true;

            return backupRealizado;
        }

        public bool RealizarRestore(string nombreBase, string rutaBackup)
        {
            bool restoreRealizado = false;

            _dataAccess.ExecuteCommandText = $"USE [master] " +
                                    $"ALTER DATABASE [{nombreBase}] SET OFFLINE WITH ROLLBACK IMMEDIATE  " +
                                    $"RESTORE DATABASE [{nombreBase}] FROM DISK = '{rutaBackup}' WITH REPLACE  " +
                                    $"ALTER DATABASE [{nombreBase}] SET ONLINE";

            _dataAccess.ExecuteNonQueryNonTransaction();

            restoreRealizado = true;
            return restoreRealizado;
        }
    }
}
