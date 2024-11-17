using iTextSharp.text;
using iTextSharp.text.pdf;
using MidMarket.Entities;
using System;
using System.IO;
using System.Web.Services;

namespace MidMarket.UI.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class GeneradorPdf : System.Web.Services.WebService
    {
        [WebMethod]
        public byte[] GenerarPdfCompra(TransaccionCompra compra)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 36, 36, 54, 54);
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                Font tituloGrandeFuente = FontFactory.GetFont("Arial", 24, Font.BOLD, BaseColor.BLACK);
                Font tituloFuente = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK);
                Font headerFuente = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
                Font celdaBoldFuente = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
                Font celdaFuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font smallFont = FontFactory.GetFont("Arial", 8, BaseColor.BLACK);

                var espaciado = new Paragraph(" ") { SpacingBefore = 10, SpacingAfter = 10 };

                Paragraph tituloPrincipal = new Paragraph("MIDMARKET", tituloGrandeFuente)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                doc.Add(tituloPrincipal);

                PdfPTable encabezado = new PdfPTable(3) { WidthPercentage = 100 };
                encabezado.SetWidths(new float[] { 1f, 2f, 1f });

                encabezado.AddCell(new PdfPCell(new Phrase("ORIGINAL", headerFuente))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                encabezado.AddCell(new PdfPCell(new Phrase("A", tituloFuente))
                {
                    Border = Rectangle.BOX,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                encabezado.AddCell(new PdfPCell(new Phrase($"Factura Compra N°: {compra.Id}", celdaFuente))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });

                doc.Add(encabezado);
                doc.Add(espaciado);

                PdfPTable datosPrincipales = new PdfPTable(2) { WidthPercentage = 100 };
                datosPrincipales.SetWidths(new float[] { 1.5f, 3.5f });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Razón Social:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("MIDMARKET", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("CUIT:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("30-12345678-9", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Domicilio Comercial:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("N/A", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Condición frente al IVA:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("Responsable Inscripto", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Fecha de Emisión:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase($"{compra.Fecha:dd/MM/yyyy}", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Punto de Venta:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("00001", celdaFuente)) { Border = Rectangle.NO_BORDER });

                PdfPCell marcoDatos = new PdfPCell(datosPrincipales)
                {
                    Border = Rectangle.BOX,
                    Padding = 10,
                    BackgroundColor = new BaseColor(240, 240, 240)
                };

                PdfPTable datosWrapper = new PdfPTable(1) { WidthPercentage = 100 };
                datosWrapper.AddCell(marcoDatos);
                doc.Add(datosWrapper);
                doc.Add(espaciado);

                PdfPTable detalleTabla = new PdfPTable(5) { WidthPercentage = 100 };
                detalleTabla.SetWidths(new float[] { 1f, 3f, 1f, 1f, 1f });

                BaseColor grisEncabezado = new BaseColor(224, 224, 224);
                detalleTabla.AddCell(new PdfPCell(new Phrase("Código", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Activo", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Cantidad", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Precio Unit.", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Subtotal", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });

                foreach (var detalle in compra.Detalle)
                {
                    var producto = detalle.Activo;
                    detalleTabla.AddCell(new PdfPCell(new Phrase(producto.Id.ToString(), celdaFuente)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    detalleTabla.AddCell(new PdfPCell(new Phrase(producto.Nombre, celdaFuente)) { HorizontalAlignment = Element.ALIGN_LEFT });
                    detalleTabla.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), celdaFuente)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    if (detalle.Activo is Accion accion)
                    {
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${accion.Precio:N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${(detalle.Cantidad * accion.Precio):N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else if (detalle.Activo is Bono bono)
                    {
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${bono.ValorNominal:N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${(detalle.Cantidad * bono.ValorNominal):N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                }

                detalleTabla.AddCell(new PdfPCell(new Phrase($"Importe Total: ${compra.Total:N2}", headerFuente))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = Rectangle.TOP_BORDER,
                    Colspan = 5
                });

                PdfPCell marcoTabla = new PdfPCell(detalleTabla)
                {
                    Border = Rectangle.BOX,
                    Padding = 10
                };

                PdfPTable tablaWrapper = new PdfPTable(1) { WidthPercentage = 100 };
                tablaWrapper.AddCell(marcoTabla);
                doc.Add(tablaWrapper);
                doc.Add(espaciado);

                PdfPTable piePagina = new PdfPTable(1) { WidthPercentage = 100 };
                PdfPTable contenidoPie = new PdfPTable(1) { WidthPercentage = 100 };

                contenidoPie.AddCell(new PdfPCell(new Phrase($"CAE N°: 123456789\nFecha de Vto CAE: {DateTime.Now.AddDays(30):dd/MM/yyyy}", smallFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });
                contenidoPie.AddCell(new PdfPCell(new Phrase($"El total de este comprobante está expresado en moneda de curso legal. Importe final ${compra.Total:N2}", smallFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingTop = 10
                });

                piePagina.AddCell(new PdfPCell(contenidoPie)
                {
                    Border = Rectangle.BOX,
                    Padding = 10
                });

                doc.Add(new Paragraph(" ") { SpacingBefore = 200 });
                doc.Add(piePagina);

                doc.Close();

                return memoryStream.ToArray();
            }
        }

        [WebMethod]
        public byte[] GenerarPdfVenta(TransaccionVenta venta)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 36, 36, 54, 54);
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                Font tituloGrandeFuente = FontFactory.GetFont("Arial", 24, Font.BOLD, BaseColor.BLACK);
                Font tituloFuente = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK);
                Font headerFuente = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
                Font celdaBoldFuente = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
                Font celdaFuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font smallFont = FontFactory.GetFont("Arial", 8, BaseColor.BLACK);

                var espaciado = new Paragraph(" ") { SpacingBefore = 10, SpacingAfter = 10 };

                Paragraph tituloPrincipal = new Paragraph("MIDMARKET", tituloGrandeFuente)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                doc.Add(tituloPrincipal);

                PdfPTable encabezado = new PdfPTable(3) { WidthPercentage = 100 };
                encabezado.SetWidths(new float[] { 1f, 2f, 1f });

                encabezado.AddCell(new PdfPCell(new Phrase("ORIGINAL", headerFuente))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT
                });
                encabezado.AddCell(new PdfPCell(new Phrase("A", tituloFuente))
                {
                    Border = Rectangle.BOX,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
                encabezado.AddCell(new PdfPCell(new Phrase($"Factura Venta N°: {venta.Id}", celdaFuente))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });

                doc.Add(encabezado);
                doc.Add(espaciado);

                PdfPTable datosPrincipales = new PdfPTable(2) { WidthPercentage = 100 };
                datosPrincipales.SetWidths(new float[] { 1.5f, 3.5f });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Razón Social:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("MIDMARKET", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("CUIT:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("30-12345678-9", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Domicilio Comercial:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("N/A", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Condición frente al IVA:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("Responsable Inscripto", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Fecha de Emisión:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase($"{venta.Fecha:dd/MM/yyyy}", celdaFuente)) { Border = Rectangle.NO_BORDER });

                datosPrincipales.AddCell(new PdfPCell(new Phrase("Punto de Venta:", celdaBoldFuente)) { Border = Rectangle.NO_BORDER });
                datosPrincipales.AddCell(new PdfPCell(new Phrase("00001", celdaFuente)) { Border = Rectangle.NO_BORDER });

                PdfPCell marcoDatos = new PdfPCell(datosPrincipales)
                {
                    Border = Rectangle.BOX,
                    Padding = 10,
                    BackgroundColor = new BaseColor(240, 240, 240)
                };

                PdfPTable datosWrapper = new PdfPTable(1) { WidthPercentage = 100 };
                datosWrapper.AddCell(marcoDatos);
                doc.Add(datosWrapper);
                doc.Add(espaciado);

                PdfPTable detalleTabla = new PdfPTable(5) { WidthPercentage = 100 };
                detalleTabla.SetWidths(new float[] { 1f, 3f, 1f, 1f, 1f });

                BaseColor grisEncabezado = new BaseColor(224, 224, 224);
                detalleTabla.AddCell(new PdfPCell(new Phrase("Código", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Activo", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Cantidad", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Precio Unit.", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Subtotal", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });

                foreach (var detalle in venta.Detalle)
                {
                    var producto = detalle.Activo;
                    detalleTabla.AddCell(new PdfPCell(new Phrase(producto.Id.ToString(), celdaFuente)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    detalleTabla.AddCell(new PdfPCell(new Phrase(producto.Nombre, celdaFuente)) { HorizontalAlignment = Element.ALIGN_LEFT });
                    detalleTabla.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), celdaFuente)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    if (detalle.Activo is Accion accion)
                    {
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${accion.Precio:N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${(detalle.Cantidad * accion.Precio):N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                    else if (detalle.Activo is Bono bono)
                    {
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${bono.ValorNominal:N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                        detalleTabla.AddCell(new PdfPCell(new Phrase($"${(detalle.Cantidad * bono.ValorNominal):N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    }
                }

                detalleTabla.AddCell(new PdfPCell(new Phrase($"Importe Total: ${venta.Total:N2}", headerFuente))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = Rectangle.TOP_BORDER,
                    Colspan = 5
                });

                PdfPCell marcoTabla = new PdfPCell(detalleTabla)
                {
                    Border = Rectangle.BOX,
                    Padding = 10
                };

                PdfPTable tablaWrapper = new PdfPTable(1) { WidthPercentage = 100 };
                tablaWrapper.AddCell(marcoTabla);
                doc.Add(tablaWrapper);
                doc.Add(espaciado);

                PdfPTable piePagina = new PdfPTable(1) { WidthPercentage = 100 };
                PdfPTable contenidoPie = new PdfPTable(1) { WidthPercentage = 100 };

                contenidoPie.AddCell(new PdfPCell(new Phrase($"CAE N°: 123456789\nFecha de Vto CAE: {DateTime.Now.AddDays(30):dd/MM/yyyy}", smallFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });
                contenidoPie.AddCell(new PdfPCell(new Phrase($"El total de este comprobante está expresado en moneda de curso legal. Importe final ${venta.Total:N2}", smallFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    PaddingTop = 10
                });

                piePagina.AddCell(new PdfPCell(contenidoPie)
                {
                    Border = Rectangle.BOX,
                    Padding = 10
                });

                doc.Add(new Paragraph(" ") { SpacingBefore = 200 });
                doc.Add(piePagina);

                doc.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
