using MidMarket.Business.Interfaces;
using MidMarket.Entities.DTOs;
using MidMarket.XML;
using System.Collections.Generic;
using System.Web.Services;
using Unity;

namespace MidMarket.UI.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class EstadisticaActivos : System.Web.Services.WebService
    {
        private readonly IActivoService _activoService;

        public EstadisticaActivos()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
        }


        [WebMethod]
        public List<ActivosCompradosDTO> CalcularActivosMasCompradosCantidad()
        {
            List<ActivosCompradosDTO> activos = _activoService.GetActivosCompradosCantidad();

            if (activos != null && activos.Count > 0)
            {
                ComprasXML.GenerarXMLActivosPorCantidad(activos);
            }

            return activos;
        }

        [WebMethod]
        public List<ActivosCompradosDTO> CalcularActivosMasCompradosTotal()
        {
            List<ActivosCompradosDTO> activos = _activoService.GetActivosCompradosTotal();

            if (activos != null && activos.Count > 0)
            {
                ComprasXML.GenerarXMLActivosPorTotal(activos);
            }

            return activos;
        }
    }
}
