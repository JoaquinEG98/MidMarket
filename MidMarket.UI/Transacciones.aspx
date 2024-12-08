﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transacciones.aspx.cs" Inherits="MidMarket.UI.Transacciones" MasterPageFile="~/Site.Master" Title="Mis Transacciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-transacciones">
        <div id="divCompras" runat="server">
            <h2 data-etiqueta="texto_MisCompras">Mis Compras</h2>
            <table id="tablaCompras">
                <thead>
                    <tr>
                        <th data-etiqueta="table_Numero">Número</th>
                        <th data-etiqueta="table_Fecha">Fecha</th>
                        <th data-etiqueta="table_Total">Total</th>
                        <th data-etiqueta="table_Accion">Acción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var compra in Compras)
                        { %>
                    <tr>
                        <td><%= compra.Id %></td>
                        <td><%= compra.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %></td>
                        <td>$<%= compra.Total.ToString("N2") %></td>
                        <td>
                            <button type="button" class="submit-btn-transacciones ver-detalle-btn" data-etiqueta="btn_VerFactura" onclick="abrirModalDetalleCompra('<%= compra.Id %>', '<%= compra.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %>')">Ver Factura</button>
                            <button type="button" class="submit-btn-transacciones descargar-btn" data-etiqueta="btn_Descargar" onclick="window.location.href='<%= ResolveUrl("~/Transacciones.aspx?descargarfacturacompra=" + compra.Id) %>'">Descargar</button>
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>

        <div id="divVentas" runat="server">
            <h2 class="titulo-ventas" data-etiqueta="texto_MisVentas">Mis Ventas</h2>
            <table id="tablaVentas">
                <thead>
                    <tr>
                        <th data-etiqueta="table_Numero">Número</th>
                        <th data-etiqueta="table_Fecha">Fecha</th>
                        <th data-etiqueta="table_Total">Total</th>
                        <th data-etiqueta="table_Accion">Acción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var venta in Ventas)
                        { %>
                    <tr>
                        <td><%= venta.Id %></td>
                        <td><%= venta.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %></td>
                        <td>$<%= venta.Total.ToString("N2") %></td>
                        <td>
                            <button type="button" class="submit-btn-transacciones ver-detalle-btn" data-etiqueta="btn_VerFactura" onclick="abrirModalDetalleVenta('<%= venta.Id %>', '<%= venta.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %>')">Ver Factura</button>
                            <button type="button" class="submit-btn-transacciones descargar-btn" data-etiqueta="btn_Descargar" onclick="window.location.href='<%= ResolveUrl("~/Transacciones.aspx?descargarfacturaventa=" + venta.Id) %>'">Descargar</button>
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>

        <asp:Literal ID="ltlNoTransacciones" runat="server" Visible="false">
            <div class="mensaje-carrito-vacio">
                <img src="images/carritovacio.png" alt="Carrito vacío" class="img-carrito-vacio" />
                <p data-etiqueta="mensaje_NoTransacciones">No tenés transacciones registradas.</p>
                <p data-etiqueta="mensaje_ComenzaInvertir">Comenzá a invertir ahora mismo en la sección <a href="Compra.aspx" class="link-compra-activos" data-etiqueta="link_CompraActivos">Compra de Activos</a></p>
            </div>
        </asp:Literal>
    </div>

    <div id="transaccionesModal" class="transacciones-modal">
        <div class="transacciones-modal-content">
            <div class="transacciones-modal-header">
                <h5 class="transacciones-modal-title" data-etiqueta="modal_TituloFactura">Factura de la Transacción</h5>
                <span class="transacciones-close" onclick="cerrarModal()">&times;</span>
            </div>
            <div class="transacciones-modal-body">
                <div class="factura-info">
                    <p><strong data-etiqueta="modal_Fecha">Fecha:</strong> <span id="facturaFecha"></span></p>
                    <p><strong data-etiqueta="modal_NumeroFactura">Factura N°:</strong> <span id="facturaNumero"></span></p>
                </div>

                <div id="accionesSection" style="display: none;">
                    <h3 class="section-title" data-etiqueta="section_Acciones">Acciones</h3>
                    <table id="tablaAcciones">
                        <thead>
                            <tr>
                                <th data-etiqueta="table_NombreActivo">Nombre de Activo</th>
                                <th data-etiqueta="table_Simbolo">Símbolo</th>
                                <th data-etiqueta="table_Cantidad">Cantidad</th>
                                <th data-etiqueta="table_Precio">Precio</th>
                                <th data-etiqueta="table_TotalActivo">Total</th>
                            </tr>
                        </thead>
                        <tbody id="detalleAccionesContainer">
                        </tbody>
                    </table>
                </div>

                <div id="separator" style="display: none;" class="separator"></div>

                <div id="bonosSection" style="display: none;">
                    <h3 class="section-title" data-etiqueta="section_Bonos">Bonos</h3>
                    <table id="tablaBonos">
                        <thead>
                            <tr>
                                <th data-etiqueta="table_NombreActivo">Nombre de Activo</th>
                                <th data-etiqueta="table_ValorNominal">Valor Nominal</th>
                                <th data-etiqueta="table_TasaInteres">Tasa de Interés</th>
                                <th data-etiqueta="table_Cantidad">Cantidad</th>
                                <th data-etiqueta="table_Precio">Precio</th>
                                <th data-etiqueta="table_TotalActivo">Total</th>
                            </tr>
                        </thead>
                        <tbody id="detalleBonosContainer">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var comprasData = <%= ViewState["ComprasJson"] %>;
        var ventasData = <%= ViewState["VentasJson"] %>;

        function abrirModalDetalleCompra(compraId, compraFecha) {
            event.preventDefault();
            event.stopPropagation();

            var compra = comprasData.find(c => c.Id == compraId);
            if (!compra) {
                console.log("No se encontró la compra con ID:", compraId);
                return;
            }

            document.getElementById('facturaFecha').textContent = compraFecha;
            document.getElementById('facturaNumero').textContent = compraId;

            var detalleAccionesContainer = document.getElementById('detalleAccionesContainer');
            var detalleBonosContainer = document.getElementById('detalleBonosContainer');
            var accionesSection = document.getElementById('accionesSection');
            var bonosSection = document.getElementById('bonosSection');
            var separator = document.getElementById('separator');

            detalleAccionesContainer.innerHTML = '';
            detalleBonosContainer.innerHTML = '';
            accionesSection.style.display = 'none';
            bonosSection.style.display = 'none';
            separator.style.display = 'none';

            var tieneAcciones = false;
            var tieneBonos = false;

            compra.Detalle.forEach(detalle => {
                var esAccion = detalle.Activo.Id_Accion !== undefined && detalle.Activo.Id_Accion !== 0;
                var esBono = detalle.Activo.Id_Bono !== undefined && detalle.Activo.Id_Bono !== 0;

                if (esAccion) {
                    tieneAcciones = true;
                    var rowHtmlAccion = `
                        <tr>
                            <td>${detalle.Activo.Nombre || 'N/A'}</td>
                            <td>${detalle.Activo.Simbolo || 'N/A'}</td>
                            <td>${detalle.Cantidad || 'N/A'}</td>
                            <td>$${detalle.Activo.Precio.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td>$${detalle.Precio.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        </tr>
                    `;
                    detalleAccionesContainer.innerHTML += rowHtmlAccion;
                } else if (esBono) {
                    tieneBonos = true;
                    var rowHtmlBono = `
                        <tr>
                            <td>${detalle.Activo.Nombre || 'N/A'}</td>
                            <td>$${detalle.Activo.ValorNominal.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td>${detalle.Activo.TasaInteres !== undefined ? detalle.Activo.TasaInteres.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '%' : 'N/A'}</td>
                            <td>${detalle.Cantidad || 'N/A'}</td>
                            <td>$${detalle.Precio.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td>$${detalle.Precio.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        </tr>
                    `;
                    detalleBonosContainer.innerHTML += rowHtmlBono;
                }
            });

            if (tieneAcciones) {
                accionesSection.style.display = 'block';
            }
            if (tieneBonos) {
                bonosSection.style.display = 'block';
            }
            if (tieneAcciones && tieneBonos) {
                separator.style.display = 'block';
            }

            document.getElementById('transaccionesModal').style.display = 'block';
        }

        function abrirModalDetalleVenta(ventaId, ventaFecha) {
            event.preventDefault();
            event.stopPropagation();

            var venta = ventasData.find(v => v.Id == ventaId);
            if (!venta) {
                console.log("No se encontró la venta con ID:", ventaId);
                return;
            }

            document.getElementById('facturaFecha').textContent = ventaFecha;
            document.getElementById('facturaNumero').textContent = ventaId;

            var detalleAccionesContainer = document.getElementById('detalleAccionesContainer');
            var detalleBonosContainer = document.getElementById('detalleBonosContainer');
            var accionesSection = document.getElementById('accionesSection');
            var bonosSection = document.getElementById('bonosSection');
            var separator = document.getElementById('separator');

            detalleAccionesContainer.innerHTML = '';
            detalleBonosContainer.innerHTML = '';
            accionesSection.style.display = 'none';
            bonosSection.style.display = 'none';
            separator.style.display = 'none';

            var tieneAcciones = false;
            var tieneBonos = false;

            venta.Detalle.forEach(detalle => {
                var esAccion = detalle.Activo.Id_Accion !== undefined && detalle.Activo.Id_Accion !== 0;
                var esBono = detalle.Activo.Id_Bono !== undefined && detalle.Activo.Id_Bono !== 0;

                if (esAccion) {
                    tieneAcciones = true;
                    var rowHtmlAccion = `
            <tr>
                <td>${detalle.Activo.Nombre || 'N/A'}</td>
                <td>${detalle.Activo.Simbolo || 'N/A'}</td>
                <td>${detalle.Cantidad || 'N/A'}</td>
                <td>$${detalle.Activo.Precio.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                <td>$${(detalle.Activo.Precio * detalle.Cantidad).toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
            </tr>
        `;
                    detalleAccionesContainer.innerHTML += rowHtmlAccion;
                } else if (esBono) {
                    tieneBonos = true;
                    var rowHtmlBono = `
            <tr>
                <td>${detalle.Activo.Nombre || 'N/A'}</td>
                <td>$${detalle.Activo.ValorNominal.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                <td>${detalle.Activo.TasaInteres !== undefined ? detalle.Activo.TasaInteres.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + '%' : 'N/A'}</td>
                <td>${detalle.Cantidad || 'N/A'}</td>
                <td>$${detalle.Activo.ValorNominal.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                <td>$${(detalle.Activo.ValorNominal * detalle.Cantidad).toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
            </tr>
        `;
                    detalleBonosContainer.innerHTML += rowHtmlBono;
                }
            });

            if (tieneAcciones) {
                accionesSection.style.display = 'block';
            }
            if (tieneBonos) {
                bonosSection.style.display = 'block';
            }
            if (tieneAcciones && tieneBonos) {
                separator.style.display = 'block';
            }

            document.getElementById('transaccionesModal').style.display = 'block';
        }

        function cerrarModal() {
            document.getElementById('transaccionesModal').style.display = 'none';
        }

        window.onclick = function (event) {
            var modal = document.getElementById('transaccionesModal');
            if (event.target == modal) {
                cerrarModal();
            }
        }
    </script>
</asp:Content>
