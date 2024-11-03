using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities.Observer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MidMarket.DataAccess.DAOs
{
    public class TraduccionDAO : ITraduccionDAO
    {
        public IList<Idioma> ObtenerIdiomas()
        {
            string json = Encoding.UTF8.GetString(Traduccion.Idioma);

            IList<Idioma> idiomas = JsonConvert.DeserializeObject<IList<Idioma>>(json);

            return idiomas;
        }

        public IIdioma ObtenerIdiomaDefault()
        {
            return ObtenerIdiomas().Where(i => i.Default).FirstOrDefault();
        }

        public IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma)
        {
            if (idioma == null)
                idioma = ObtenerIdiomaDefault();

            string json = Encoding.UTF8.GetString(Traduccion.Traducciones);
            IList<Entities.Observer.Traduccion> traducciones = JsonConvert.DeserializeObject<IList<Entities.Observer.Traduccion>>(json);

            var traduccionesFiltradas = traducciones
                .Where(t => t.IdiomaId == idioma.Id)
                .ToDictionary(t => t.Etiqueta, t => (ITraduccion)t);

            return traduccionesFiltradas;
        }
    }
}
