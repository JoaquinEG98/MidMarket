namespace MidMarket.Entities.DTOs
{
    public class AccionDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public string Simbolo { get; set; }
        public decimal Precio { get; set; }
        public int Id_Activo { get; set; }
    }
}
