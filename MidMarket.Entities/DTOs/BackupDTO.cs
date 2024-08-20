using System;

namespace MidMarket.Entities.DTOs
{
    public class BackupDTO
    {
        public int Id { get; set; }
        public string NombreBase { get; set; }
        public string RutaBackup { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente RealizadoPor { get; set; }
    }
}
