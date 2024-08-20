using MidMarket.Entities.DTOs;

namespace MidMarket.DataAccess.Interfaces
{
    public interface IBackupDAO
    {
        bool RealizarBackup(BackupDTO backup);
        bool RealizarRestore(string nombreBase, string rutaBackup);
    }
}
