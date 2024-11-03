using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities.Observer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace MidMarket.DataAccess.DAOs
{
    public class TraduccionDAO : ITraduccionDAO
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;
        private const string _cacheIdiomasKey = "Idiomas";
        private const string _cacheTraduccionesKey = "Traducciones";
        private readonly DateTimeOffset _tiempoCache = DateTimeOffset.Now.AddHours(1);

        public IList<Idioma> ObtenerIdiomas()
        {
            if (!_cache.Contains(_cacheIdiomasKey))
            {
                string json = Encoding.UTF8.GetString(Traduccion.Idioma);
                IList<Idioma> idiomas = JsonConvert.DeserializeObject<IList<Idioma>>(json);

                _cache.Set(_cacheIdiomasKey, idiomas, _tiempoCache);
            }

            return (IList<Idioma>)_cache.Get(_cacheIdiomasKey);
        }

        public IIdioma ObtenerIdiomaDefault()
        {
            return ObtenerIdiomas().Where(i => i.Default).FirstOrDefault();
        }

        public IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma)
        {
            if (idioma == null)
                idioma = ObtenerIdiomaDefault();

            if (!_cache.Contains(_cacheTraduccionesKey))
            {
                string json = Encoding.UTF8.GetString(Traduccion.Traducciones);
                IList<Entities.Observer.Traduccion> traducciones = JsonConvert.DeserializeObject<IList<Entities.Observer.Traduccion>>(json);

                var cacheData = traducciones
                    .Where(t => t.IdiomaId == idioma.Id)
                    .ToDictionary(t => t.Etiqueta, t => (ITraduccion)t);

                _cache.Set(_cacheTraduccionesKey, cacheData, _tiempoCache);
            }

            var traduccionesCache = (IDictionary<string, ITraduccion>)_cache.Get(_cacheTraduccionesKey);

            var traduccionesFiltradas = traduccionesCache
                .Where(t => t.Value.IdiomaId == idioma.Id)
                .ToDictionary(t => t.Key, t => t.Value);

            return traduccionesFiltradas;
        }

    }
}
