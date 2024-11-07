namespace MidMarket.Entities.DTOs
{
    public class BonoDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public decimal ValorNominal { get; set; }
        public float TasaInteres { get; set; }
        public int Id_Activo { get; set; }
    }
}
