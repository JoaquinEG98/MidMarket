namespace MidMarket.Business.Interfaces
{
    public interface IBackupService
    {
        void RealizarBackup(string rutaBackup);
        void RealizarRestore(string rutaBackup);
    }
}
