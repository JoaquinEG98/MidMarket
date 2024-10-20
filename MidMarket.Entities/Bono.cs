namespace MidMarket.Entities
{
    public class Bono : Activo
    {
        public int Id_Bono { get; set; }
        public decimal ValorNominal { get; set; }
        public float TasaInteres { get; set; }       
    }
}
