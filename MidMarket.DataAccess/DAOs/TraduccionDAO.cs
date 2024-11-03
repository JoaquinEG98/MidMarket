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
        private const string _cacheKey = "Traducciones";
        private readonly DateTimeOffset _tiempoCache = DateTimeOffset.Now.AddHours(1);

        public IList<Idioma> ObtenerIdiomas()
        {
            if (!_cache.Contains(_cacheKey))
            {
                string json = Encoding.UTF8.GetString(Traduccion.Idioma);
                IList<Idioma> idiomas = JsonConvert.DeserializeObject<IList<Idioma>>(json);

                _cache.Set(_cacheKey, idiomas, _tiempoCache);
            }

            return (IList<Idioma>)_cache.Get(_cacheKey);
        }

        public IIdioma ObtenerIdiomaDefault()
        {
            return ObtenerIdiomas().Where(i => i.Default).FirstOrDefault();
        }

        public IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma)
        {
            if (!_cache.Contains(_cacheKey))
            {
                string json = Encoding.UTF8.GetString(Traduccion.Traducciones);
                IList<Entities.Observer.Traduccion> traducciones = JsonConvert.DeserializeObject<IList<Entities.Observer.Traduccion>>(json);

                var cacheData = traducciones.ToDictionary(t => $"{t.IdiomaId}_{t.Etiqueta}", t => (ITraduccion)t);
                _cache.Set(_cacheKey, cacheData, _tiempoCache);
            }

            if (idioma == null)
                idioma = ObtenerIdiomaDefault();

            var traduccionesCache = (IDictionary<string, ITraduccion>)_cache.Get(_cacheKey);

            var traduccionesFiltradas = traduccionesCache
                .Where(t => t.Key.StartsWith($"{idioma.Id}_"))
                .ToDictionary(t => t.Key.Split('_')[1], t => t.Value);

            return traduccionesFiltradas;
        }
    }
}
