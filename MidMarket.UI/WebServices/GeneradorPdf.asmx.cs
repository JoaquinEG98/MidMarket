﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using MidMarket.Entities;
using System;
using System.IO;
using System.Linq;
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

                // Fuentes
                Font tituloFuente = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK);
                Font headerFuente = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
                Font celdaBoldFuente = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
                Font celdaFuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);
                Font smallFont = FontFactory.GetFont("Arial", 8, BaseColor.BLACK);

                // Espaciado
                var espaciado = new Paragraph(" ") { SpacingBefore = 10, SpacingAfter = 10 };

                // Encabezado principal con "ORIGINAL A"
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
                encabezado.AddCell(new PdfPCell(new Phrase($"Factura N°: {compra.Id}", celdaFuente))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });

                doc.Add(encabezado);
                doc.Add(espaciado);

                // Información principal (MidMarket) enmarcada
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

                // Tabla de productos
                PdfPTable detalleTabla = new PdfPTable(5) { WidthPercentage = 100 };
                detalleTabla.SetWidths(new float[] { 1f, 3f, 1f, 1f, 1f });

                BaseColor grisEncabezado = new BaseColor(224, 224, 224);
                detalleTabla.AddCell(new PdfPCell(new Phrase("Código", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Producto/Servicio", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Cantidad", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Precio Unit.", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });
                detalleTabla.AddCell(new PdfPCell(new Phrase("Subtotal", headerFuente)) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = grisEncabezado });

                foreach (var detalle in compra.Detalle)
                {
                    var producto = detalle.Activo;
                    detalleTabla.AddCell(new PdfPCell(new Phrase(producto.Id.ToString(), celdaFuente)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    detalleTabla.AddCell(new PdfPCell(new Phrase(producto.Nombre, celdaFuente)) { HorizontalAlignment = Element.ALIGN_LEFT });
                    detalleTabla.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), celdaFuente)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    detalleTabla.AddCell(new PdfPCell(new Phrase($"${detalle.Precio:N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    detalleTabla.AddCell(new PdfPCell(new Phrase($"${(detalle.Cantidad * detalle.Precio):N2}", celdaFuente)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                }

                detalleTabla.AddCell(new PdfPCell(new Phrase($"Importe Total: ${compra.Detalle.Sum(d => d.Cantidad * d.Precio):N2}", headerFuente))
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

                // Pie de página
                PdfPTable piePagina = new PdfPTable(1) { WidthPercentage = 100 };
                PdfPTable contenidoPie = new PdfPTable(1) { WidthPercentage = 100 };

                contenidoPie.AddCell(new PdfPCell(new Phrase($"CAE N°: 123456789\nFecha de Vto CAE: {DateTime.Now.AddDays(30):dd/MM/yyyy}", smallFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });
                contenidoPie.AddCell(new PdfPCell(new Phrase($"El total de este comprobante está expresado en moneda de curso legal. Importe final: ${compra.Detalle.Sum(d => d.Cantidad * d.Precio):N2}", smallFont))
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

                doc.Add(new Paragraph(" ") { SpacingBefore = 300 }); // Empujar hacia abajo
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
                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                Font tituloFuente = FontFactory.GetFont("Arial", 16, Font.BOLD, new BaseColor(74, 20, 140)); // Violeta
                Font headerFuente = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
                Font celdaFuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);

                Paragraph titulo = new Paragraph("Factura de la Venta", tituloFuente);
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.SpacingAfter = 15f;
                doc.Add(titulo);

                doc.Add(new Paragraph(" "));
                PdfPTable infoTabla = new PdfPTable(2) { WidthPercentage = 100 };
                infoTabla.AddCell(new PdfPCell(new Phrase("Fecha:", headerFuente)) { Border = 0 });
                infoTabla.AddCell(new PdfPCell(new Phrase(venta.Fecha.ToString("dd/MM/yyyy HH:mm:ss"), celdaFuente)) { Border = 0 });
                infoTabla.AddCell(new PdfPCell(new Phrase("Factura N°:", headerFuente)) { Border = 0 });
                infoTabla.AddCell(new PdfPCell(new Phrase(venta.Id.ToString(), celdaFuente)) { Border = 0 });
                doc.Add(infoTabla);

                doc.Add(new Paragraph(" "));

                if (venta.Detalle.Any(d => d.Activo is Accion))
                {
                    Paragraph accionesTitulo = new Paragraph("Acciones", headerFuente)
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 8f
                    };
                    accionesTitulo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(accionesTitulo);

                    PdfPTable accionesTabla = new PdfPTable(5) { WidthPercentage = 100 };
                    accionesTabla.AddCell(new PdfPCell(new Phrase("Nombre de Activo", headerFuente)));
                    accionesTabla.AddCell(new PdfPCell(new Phrase("Símbolo", headerFuente)));
                    accionesTabla.AddCell(new PdfPCell(new Phrase("Cantidad", headerFuente)));
                    accionesTabla.AddCell(new PdfPCell(new Phrase("Precio", headerFuente)));
                    accionesTabla.AddCell(new PdfPCell(new Phrase("Total", headerFuente)));

                    foreach (var detalle in venta.Detalle.Where(d => d.Activo is Accion))
                    {
                        var accion = detalle.Activo as Accion;
                        accionesTabla.AddCell(new PdfPCell(new Phrase(accion.Nombre, celdaFuente)));
                        accionesTabla.AddCell(new PdfPCell(new Phrase(accion.Simbolo ?? "N/A", celdaFuente)));
                        accionesTabla.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), celdaFuente)));
                        accionesTabla.AddCell(new PdfPCell(new Phrase($"${accion.Precio.ToString("N2")}", celdaFuente)));
                        accionesTabla.AddCell(new PdfPCell(new Phrase($"${detalle.Precio.ToString("N2")}", celdaFuente)));
                    }

                    doc.Add(accionesTabla);
                }

                if (venta.Detalle.Any(d => d.Activo is Accion) && venta.Detalle.Any(d => d.Activo is Bono))
                {
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph(" "));
                }

                if (venta.Detalle.Any(d => d.Activo is Bono))
                {
                    Paragraph bonosTitulo = new Paragraph("Bonos", headerFuente)
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 8f
                    };
                    bonosTitulo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(bonosTitulo);

                    PdfPTable bonosTabla = new PdfPTable(6) { WidthPercentage = 100 };
                    bonosTabla.AddCell(new PdfPCell(new Phrase("Nombre de Activo", headerFuente)));
                    bonosTabla.AddCell(new PdfPCell(new Phrase("Valor Nominal", headerFuente)));
                    bonosTabla.AddCell(new PdfPCell(new Phrase("Tasa de Interés", headerFuente)));
                    bonosTabla.AddCell(new PdfPCell(new Phrase("Cantidad", headerFuente)));
                    bonosTabla.AddCell(new PdfPCell(new Phrase("Precio", headerFuente)));
                    bonosTabla.AddCell(new PdfPCell(new Phrase("Total", headerFuente)));

                    foreach (var detalle in venta.Detalle.Where(d => d.Activo is Bono))
                    {
                        var bono = detalle.Activo as Bono;
                        bonosTabla.AddCell(new PdfPCell(new Phrase(bono.Nombre, celdaFuente)));
                        bonosTabla.AddCell(new PdfPCell(new Phrase($"${bono.ValorNominal.ToString("N2")}", celdaFuente)));
                        bonosTabla.AddCell(new PdfPCell(new Phrase($"{bono.TasaInteres.ToString("N2")}%", celdaFuente)));
                        bonosTabla.AddCell(new PdfPCell(new Phrase(detalle.Cantidad.ToString(), celdaFuente)));
                        bonosTabla.AddCell(new PdfPCell(new Phrase($"${detalle.Precio.ToString("N2")}", celdaFuente)));
                        bonosTabla.AddCell(new PdfPCell(new Phrase($"${detalle.Precio.ToString("N2")}", celdaFuente)));
                    }

                    doc.Add(bonosTabla);
                }

                decimal total = venta.Detalle.Sum(d => d.Precio);
                doc.Add(new Paragraph(" "));
                Paragraph totalParrafo = new Paragraph($"Total: ${total.ToString("N2")}", headerFuente)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingBefore = 15f
                };
                doc.Add(totalParrafo);

                doc.Close();

                byte[] bytes = memoryStream.ToArray();
                return bytes;
            }
        }
    }
}
