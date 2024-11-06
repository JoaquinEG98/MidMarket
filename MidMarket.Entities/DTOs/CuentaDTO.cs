namespace MidMarket.Entities.DTOs
{
    public class CuentaDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public long NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
    }
}
