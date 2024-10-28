namespace MidMarket.Entities
{
    public class Accion : Activo
    {
        public int Id_Accion { get; set; }
        public string Simbolo { get; set; }
        public decimal Precio { get; set; }
    }
}
