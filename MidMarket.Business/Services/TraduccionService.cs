using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities.Observer;
using System;
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
            try
            {
                IList<Idioma> idiomas = _traduccionDataAccess.ObtenerIdiomas();

                return idiomas;
            }
            catch (Exception) { throw new Exception("Hubo un error al querer obtener los idiomas."); }
        }

        public IIdioma ObtenerIdiomaDefault()
        {
            try
            {
                IIdioma idioma = _traduccionDataAccess.ObtenerIdiomaDefault();

                return idioma;
            }
            catch (Exception) { throw new Exception("Hubo un error al querer obtener los idiomas."); }
        }
    }
}
