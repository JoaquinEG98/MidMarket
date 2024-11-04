namespace MidMarket.Entities.Observer
{
    public class Traduccion : ITraduccion
    {
        public int Id { get; set; }
        public int IdiomaId { get; set; }
        public string Etiqueta { get; set; }
        public string Texto { get; set; }
    }
}
