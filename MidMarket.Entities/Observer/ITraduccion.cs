namespace MidMarket.Entities.Observer
{
    public interface ITraduccion
    {
        int IdiomaId { get; set; }
        string Etiqueta { get; set; }
        string Texto { get; set; }
    }
}
