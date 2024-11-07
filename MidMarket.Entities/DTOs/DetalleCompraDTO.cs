namespace MidMarket.Entities.DTOs
{
    public class DetalleCompraDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public int Id_Activo { get; set; }
        public int Id_Compra { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
