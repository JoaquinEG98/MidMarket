using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MidMarket.XML
{
    public static class VentasXML
    {
        public static void GenerarXMLActivosPorCantidad(List<ActivosVendidosDTO> activos)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ActivosVendidosDTO>));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, activos);
                string xmlResult = writer.ToString();

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Ventas//ActivosVendidosCantidad.xml");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, xmlResult);
            }
        }

        public static void GenerarXMLActivosPorTotal(List<ActivosVendidosDTO> activos)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ActivosVendidosDTO>));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, activos);
                string xmlResult = writer.ToString();

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Ventas//ActivosVendidosTotal.xml");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, xmlResult);
            }
        }
    }
}
