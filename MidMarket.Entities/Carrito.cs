namespace MidMarket.Entities
{
    public class Carrito
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Activo Activo { get; set; }
        public Cliente Cliente { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
