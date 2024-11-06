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
        private const string _cacheMensajesKey = "Mensajes";
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

            string cacheKeyIdioma = $"{_cacheTraduccionesKey}_{idioma.Id}";

            if (!_cache.Contains(cacheKeyIdioma))
            {
                string json = Encoding.UTF8.GetString(Traduccion.Traducciones);
                IList<Entities.Observer.Traduccion> traducciones = JsonConvert.DeserializeObject<IList<Entities.Observer.Traduccion>>(json);

                var cacheData = traducciones
                    .Where(t => t.IdiomaId == idioma.Id)
                    .ToDictionary(t => t.Etiqueta, t => (ITraduccion)t);

                _cache.Set(cacheKeyIdioma, cacheData, _tiempoCache);
            }

            var traduccionesCache = (IDictionary<string, ITraduccion>)_cache.Get(cacheKeyIdioma);

            return traduccionesCache;
        }

        public string ObtenerMensaje(IIdioma idioma, string etiqueta)
        {
            if (idioma == null)
                idioma = ObtenerIdiomaDefault();

            string cacheKeyIdioma = $"{_cacheMensajesKey}_{idioma.Id}";

            if (!_cache.Contains(cacheKeyIdioma))
            {
                string json = Encoding.UTF8.GetString(Traduccion.Mensajes);
                IList<Entities.Observer.Traduccion> mensajes = JsonConvert.DeserializeObject<IList<Entities.Observer.Traduccion>>(json);

                var cacheData = mensajes
                    .Where(t => t.IdiomaId == idioma.Id)
                    .ToDictionary(t => t.Etiqueta, t => (ITraduccion)t);

                _cache.Set(cacheKeyIdioma, cacheData, _tiempoCache);
            }

            var mensajesCache = (IDictionary<string, ITraduccion>)_cache.Get(cacheKeyIdioma);

            if (mensajesCache.TryGetValue(etiqueta, out ITraduccion traduccion))
            {
                return traduccion.Texto;
            }

            return null;
        }

        public void LimpiarCache()
        {
            _cache.Remove(_cacheIdiomasKey);

            var traduccionesKeys = _cache.Where(kvp => kvp.Key.StartsWith(_cacheTraduccionesKey)).Select(kvp => kvp.Key).ToList();
            foreach (var key in traduccionesKeys)
            {
                _cache.Remove(key);
            }

            var mensajesKeys = _cache.Where(kvp => kvp.Key.StartsWith(_cacheMensajesKey)).Select(kvp => kvp.Key).ToList();
            foreach (var key in mensajesKeys)
            {
                _cache.Remove(key);
            }
        }
    }
}
