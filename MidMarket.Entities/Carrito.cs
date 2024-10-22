namespace MidMarket.Entities
{
    internal class Carrito
    {
        public int Id { get; set; }
        public Activo Activo { get; set; }
        public Cliente Cliente { get; set; }
        public int Cantidad { get; set; }
    }
}
