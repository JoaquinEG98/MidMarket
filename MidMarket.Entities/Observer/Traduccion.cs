namespace MidMarket.Entities.Observer
{
    public class Traduccion : ITraduccion
    {
        public int Id { get; set; }
        public IEtiqueta Etiqueta { get; set; }
        public string Texto { get; set; }
    }
}
