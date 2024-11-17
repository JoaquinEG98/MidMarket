using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.DTOs;
using MidMarket.Entities.Enums;
using MidMarket.Seguridad;
using System;
using System.Configuration;
using System.IO;

namespace MidMarket.Business.Services
{
    public class BackupService : IBackupService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IBackupDAO _backupDataAccess;
        private readonly IBitacoraService _bitacoraService;

        public BackupService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _backupDataAccess = DependencyResolver.Resolve<IBackupDAO>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
        }

        public void RealizarBackup(string rutaBackup)
        {
            string nombreBase = ConfigurationManager.AppSettings["base"];
            DateTime fecha = ClockWrapper.Now();
            string nombreBackup = $"{nombreBase}_{fecha.ToString("dd-MM-yyyy-HH-mm-ss")}";
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (!Directory.Exists(rutaBackup))
            {
                Directory.CreateDirectory(rutaBackup);
            }

            string rutaCompletaBackup = Path.Combine(rutaBackup, $"{nombreBackup}.bak");

            var backup = new BackupDTO()
            {
                NombreBase = nombreBase,
                RutaBackup = rutaCompletaBackup,
                Nombre = nombreBackup,
                Fecha = fecha,
                RealizadoPor = clienteLogueado
            };

            bool backupRealizado = _backupDataAccess.RealizarBackup(backup);

            if (backupRealizado)
            {
                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) realizó backup de la base de datos", Criticidad.Media, clienteLogueado);
            }
        }

        public void RealizarRestore(string rutaBackup)
        {
            string nombreBase = ConfigurationManager.AppSettings["base"];
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
            bool restoreRealizado = _backupDataAccess.RealizarRestore(nombreBase, rutaBackup);
        }
    }
}
