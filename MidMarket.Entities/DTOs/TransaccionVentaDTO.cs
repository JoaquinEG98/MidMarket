using System;

namespace MidMarket.Entities.DTOs
{
    public class TransaccionVentaDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public long Id_Cuenta { get; set; }
        public int Id_Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
    }
}
