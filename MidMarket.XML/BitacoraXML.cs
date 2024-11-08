using MidMarket.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MidMarket.XML
{
    public static class BitacoraXML
    {
        public static void GenerarXMLBitacora(List<Bitacora> movimientos)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Bitacora>));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, movimientos);
                string xmlResult = writer.ToString();

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", $"Bitacora//BitacoraXML_{DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss")}.xml");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, xmlResult);
            }
        }

        public static void GenerarExcelBitacora(List<Bitacora> movimientos)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", $"Bitacora//BitacoraExcel_{DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss")}.xls");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("<table border='1'>");
                writer.WriteLine("<tr><th>Fecha</th><th>Usuario</th><th>Criticidad</th><th>Mensaje</th></tr>");

                foreach (var movimiento in movimientos)
                {
                    writer.WriteLine("<tr>");
                    writer.WriteLine($"<td>{movimiento.Fecha:dd/MM/yyyy HH:mm}</td>");
                    writer.WriteLine($"<td>{movimiento.Cliente.RazonSocial}</td>");
                    writer.WriteLine($"<td>{movimiento.Criticidad}</td>");
                    writer.WriteLine($"<td>{movimiento.Descripcion}</td>");
                    writer.WriteLine("</tr>");
                }

                writer.WriteLine("</table>");
            }
        }

        public static void GenerarXMLLimpiarBitacora(List<Bitacora> movimientos)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Bitacora>));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, movimientos);
                string xmlResult = writer.ToString();

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", $"LimpiarBitacora//BitacoraXML_{DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss")}.xml");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, xmlResult);
            }
        }

        public static void GenerarExcelLimpiarBitacora(List<Bitacora> movimientos)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", $"LimpiarBitacora//BitacoraExcel_{DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss")}.xls");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("<table border='1'>");
                writer.WriteLine("<tr><th>Fecha</th><th>Usuario</th><th>Criticidad</th><th>Mensaje</th></tr>");

                foreach (var movimiento in movimientos)
                {
                    writer.WriteLine("<tr>");
                    writer.WriteLine($"<td>{movimiento.Fecha:dd/MM/yyyy HH:mm}</td>");
                    writer.WriteLine($"<td>{movimiento.Cliente.RazonSocial}</td>");
                    writer.WriteLine($"<td>{movimiento.Criticidad}</td>");
                    writer.WriteLine($"<td>{movimiento.Descripcion}</td>");
                    writer.WriteLine("</tr>");
                }

                writer.WriteLine("</table>");
            }
        }
    }
}
