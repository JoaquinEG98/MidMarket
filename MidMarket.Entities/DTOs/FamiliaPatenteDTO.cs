namespace MidMarket.Entities.DTOs
{
    public class FamiliaPatenteDTO : DigitoVerificadorHorizontal
    {
        public int Id_Padre { get; set; }
        public int Id_Hijo { get; set; }
    }
}
