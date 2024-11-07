namespace MidMarket.Entities.DTOs
{
    public class DetalleVentaDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public int Id_Activo { get; set; }
        public int Id_Venta { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
