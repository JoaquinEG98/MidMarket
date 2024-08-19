using MidMarket.Entities.Enums;
using System;


namespace MidMarket.Entities
{
    public class Bitacora
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public string Descripcion { get; set; }
        public Criticidad Criticidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
