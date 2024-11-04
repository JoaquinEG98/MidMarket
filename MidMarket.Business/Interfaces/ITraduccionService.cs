using MidMarket.Entities.Observer;
using System.Collections.Generic;

namespace MidMarket.Business.Interfaces
{
    public interface ITraduccionService
    {
        IList<Idioma> ObtenerIdiomas();
        IIdioma ObtenerIdiomaDefault();
        IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma);
        string ObtenerMensaje(IIdioma idioma, string etiqueta);
    }
}
