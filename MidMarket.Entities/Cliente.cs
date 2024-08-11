namespace MidMarket.Entities
{
    public class Cliente : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public double Puntaje { get; set; }
        public Cuenta Cuenta { get; set; }
    }
}
