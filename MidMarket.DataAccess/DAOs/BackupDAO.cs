using MidMarket.DataAccess.Conexion;
using MidMarket.Entities.DTOs;
using MidMarket.DataAccess.Interfaces;

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

    }
}
