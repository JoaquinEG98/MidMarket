using System;

namespace MidMarket.Entities
{
    public class TransaccionCompra
    {
        public int Id { get; set; }
        public Cuenta Cuenta { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public DetalleCompra Detalle { get; set; }
        public decimal Total { get; set; }
    }
}
