using System;
using System.Collections.Generic;

namespace MidMarket.Entities
{
    public class TransaccionCompra : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public Cuenta Cuenta { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleCompra> Detalle { get; set; }
        public decimal Total { get; set; }
    }
}
