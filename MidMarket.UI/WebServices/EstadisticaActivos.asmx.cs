using MidMarket.Business.Interfaces;
using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Services;
using System.Xml.Serialization;
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<ActivosCompradosDTO>));
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, activos);
                    string xmlResult = writer.ToString();

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "ActivosCompradosCantidad.xml");
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.WriteAllText(path, xmlResult);
                }
            }

            return activos;
        }

        [WebMethod]
        public List<ActivosCompradosDTO> CalcularActivosMasCompradosTotal()
        {
            List<ActivosCompradosDTO> activos = _activoService.GetActivosCompradosTotal();

            if (activos != null && activos.Count > 0)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ActivosCompradosDTO>));
                using (StringWriter writer = new StringWriter())
                {
                    serializer.Serialize(writer, activos);
                    string xmlResult = writer.ToString();

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "ActivosCompradosTotal.xml");
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.WriteAllText(path, xmlResult);
                }
            }

            return activos;
        }
    }
}
