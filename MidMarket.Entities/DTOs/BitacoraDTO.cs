using MidMarket.Entities.Enums;
using System;

namespace MidMarket.Entities.DTOs
{
    public class BitacoraDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public string Descripcion { get; set; }
        public Criticidad Criticidad { get; set; }
        public DateTime Fecha { get; set; }
        public bool Baja { get; set; }
    }
}
