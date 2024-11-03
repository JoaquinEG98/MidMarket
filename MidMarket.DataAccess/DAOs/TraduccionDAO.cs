using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities.Observer;
using Newtonsoft.Json;
using System.Collections.Generic;
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
    }
}
