using MidMarket.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MidMarket.XML
{
    public static class ComprasXML
    {
        public static void GenerarXMLActivosPorCantidad(List<ActivosCompradosDTO> activos)
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

        public static void GenerarXMLActivosPorTotal(List<ActivosCompradosDTO> activos)
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
    }
}
