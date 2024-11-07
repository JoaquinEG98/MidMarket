using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities.Observer;
using System.Collections.Generic;

namespace MidMarket.Business.Services
{
    public class TraduccionService : ITraduccionService
    {
        private readonly ITraduccionDAO _traduccionDataAccess;

        public TraduccionService()
        {
            _traduccionDataAccess = DependencyResolver.Resolve<ITraduccionDAO>();

        }

        public IList<Idioma> ObtenerIdiomas()
        {
            IList<Idioma> idiomas = _traduccionDataAccess.ObtenerIdiomas();

            return idiomas;
        }

        public IIdioma ObtenerIdiomaDefault()
        {
            IIdioma idioma = _traduccionDataAccess.ObtenerIdiomaDefault();

            return idioma;
        }

        public IDictionary<string, ITraduccion> ObtenerTraducciones(IIdioma idioma)
        {
            IDictionary<string, ITraduccion> traducciones = _traduccionDataAccess.ObtenerTraducciones(idioma);

            return traducciones;
        }

        public string ObtenerMensaje(IIdioma idioma, string etiqueta)
        {
            var mensaje = _traduccionDataAccess.ObtenerMensaje(idioma, etiqueta);

            return mensaje;
        }

        public void LimpiarCache()
        {
            _traduccionDataAccess.LimpiarCache();
        }
    }
}
