using MidMarket.Entities.Observer;
using System.Collections.Generic;

namespace MidMarket.DataAccess.Interfaces
{
    public interface ITraduccionDAO
    {
        IList<Idioma> ObtenerIdiomas();
        IIdioma ObtenerIdiomaDefault();
        IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma);
    }
}
