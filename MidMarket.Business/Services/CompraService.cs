using iTextSharp.text;
using iTextSharp.text.pdf;
using MidMarket.Business.Interfaces;
using MidMarket.DataAccess.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Enums;
using MidMarket.Seguridad;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;

namespace MidMarket.Business.Services
{
    public class CompraService : ICompraService
    {
        private readonly ISessionManager _sessionManager;
        private readonly IBitacoraService _bitacoraService;
        private readonly ICompraDAO _compraDataAccess;
        private readonly IUsuarioService _usuarioService;

        public CompraService()
        {
            _sessionManager = DependencyResolver.Resolve<ISessionManager>();
            _bitacoraService = DependencyResolver.Resolve<IBitacoraService>();
            _compraDataAccess = DependencyResolver.Resolve<ICompraDAO>();
            _usuarioService = DependencyResolver.Resolve<IUsuarioService>();
        }

        public void RealizarCompra(List<Carrito> carrito)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                var saldoActual = _usuarioService.GetCliente(clienteLogueado.Id).Cuenta.Saldo;
                var total = carrito.Sum(x => x.Total);

                ValidarSaldo(saldoActual, total);

                int compraId = _compraDataAccess.InsertarTransaccionCompra(clienteLogueado, total);

                foreach (var item in carrito)
                {
                    _compraDataAccess.InsertarDetalleCompra(item, compraId);
                    _compraDataAccess.InsertarActivoCliente(clienteLogueado, item);
                }

                _usuarioService.ActualizarSaldo(total);

                _bitacoraService.AltaBitacora($"{clienteLogueado.RazonSocial} ({clienteLogueado.Id}) realizó la Compra con Id: ({total}) por un total de (${total})", Criticidad.Media, clienteLogueado);

                scope.Complete();
            }
        }

        public void ValidarSaldo(decimal saldo, decimal total)
        {
            if (total > saldo)
                throw new System.Exception(Errores.ObtenerError(18));
        }

        public List<TransaccionCompra> GetCompras()
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            List<TransaccionCompra> compras = _compraDataAccess.GetCompras(clienteLogueado);

            return compras;
        }

        public void DescargarFacturaPdf(TransaccionCompra compra, System.Web.HttpResponse response)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();

                Font tituloFuente = FontFactory.GetFont("Arial", 16, Font.BOLD, new BaseColor(74, 20, 140)); // Violeta
                Font headerFuente = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
                Font celdaFuente = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);

                Paragraph titulo = new Paragraph("Factura de la Compra", tituloFuente);
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.SpacingAfter = 15f;
                doc.Add(titulo);

                doc.Add(new Paragraph(" "));
                PdfPTable infoTabla = new PdfPTable(2) { WidthPercentage = 100 };
                infoTabla.AddCell(new PdfPCell(new Phrase("Fecha:", headerFuente)) { Border = 0 });
                infoTabla.AddCell(new PdfPCell(new Phrase(compra.Fecha.ToString("dd/MM/yyyy HH:mm:ss"), celdaFuente)) { Border = 0 });
                infoTabla.AddCell(new PdfPCell(new Phrase("Factura N°:", headerFuente)) { Border = 0 });
                infoTabla.AddCell(new PdfPCell(new Phrase(compra.Id.ToString(), celdaFuente)) { Border = 0 });
                doc.Add(infoTabla);

                doc.Add(new Paragraph(" "));

                if (compra.Detalle.Any(d => d.Activo is Accion))
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

                    foreach (var detalle in compra.Detalle.Where(d => d.Activo is Accion))
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

                if (compra.Detalle.Any(d => d.Activo is Accion) && compra.Detalle.Any(d => d.Activo is Bono))
                {
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph(" "));
                }

                if (compra.Detalle.Any(d => d.Activo is Bono))
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

                    foreach (var detalle in compra.Detalle.Where(d => d.Activo is Bono))
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

                doc.Close();

                byte[] bytes = memoryStream.ToArray();
                response.Clear();
                response.ContentType = "application/pdf";
                response.AddHeader("Content-Disposition", $"attachment; filename=Factura_{compra.Id}.pdf");
                response.OutputStream.Write(bytes, 0, bytes.Length);
                response.Flush();
                response.End();
            }
        }
    }
}
