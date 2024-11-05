namespace MidMarket.Entities.DTOs
{
    public class ClienteActivoDTO : DigitoVerificadorHorizontal
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Activo { get; set; }
        public int Cantidad { get; set; }
    }
}
