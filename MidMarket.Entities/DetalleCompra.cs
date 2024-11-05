namespace MidMarket.Entities
{
    public class DetalleCompra : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public Activo Activo { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
