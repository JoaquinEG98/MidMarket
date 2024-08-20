using MidMarket.Entities.DTOs;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IBackupDAO
    {
        bool RealizarBackup(BackupDTO backup);
    }
}
