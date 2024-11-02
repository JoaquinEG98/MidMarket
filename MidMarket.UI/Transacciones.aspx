<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transacciones.aspx.cs" Inherits="MidMarket.UI.Transacciones" MasterPageFile="~/Site.Master" Title="Mis Transacciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-transacciones">
        <h2>Mis Transacciones</h2>

        <!-- Tabla de Compras -->
        <table id="tablaCompras">
            <thead>
                <tr>
                    <th>Número</th>
                    <th>Fecha</th>
                    <th>Total</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var compra in Compras) { %>
                <tr>
                    <td><%= compra.Id %></td>
                    <td><%= compra.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %></td>
                    <td>$<%= compra.Total.ToString("N2") %></td>
                    <td>
                        <button type="button" class="submit-btn-transacciones ver-detalle-btn" onclick="abrirModalDetalleCompra('<%= compra.Id %>', '<%= compra.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %>')">Ver Factura</button>
                        <button type="button" class="submit-btn-transacciones descargar-btn" onclick="window.location.href='<%= ResolveUrl("~/Transacciones.aspx?descargarfacturacompra=" + compra.Id) %>'">Descargar</button>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>

        <!-- Espacio antes de la tabla de Ventas -->
        <h2 class="titulo-ventas">Mis Ventas</h2>

        <!-- Tabla de Ventas -->
        <table id="tablaVentas">
            <thead>
                <tr>
                    <th>Número</th>
                    <th>Fecha</th>
                    <th>Total</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var venta in Ventas) { %>
                <tr>
                    <td><%= venta.Id %></td>
                    <td><%= venta.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %></td>
                    <td>$<%= venta.Total.ToString("N2") %></td>
                    <td>
                        <button type="button" class="submit-btn-transacciones ver-detalle-btn" onclick="abrirModalDetalleVenta('<%= venta.Id %>', '<%= venta.Fecha.ToString("dd/MM/yyyy HH:mm:ss") %>')">Ver Factura</button>
                        <button type="button" class="submit-btn-transacciones descargar-btn" onclick="window.location.href='<%= ResolveUrl("~/Transacciones.aspx?descargarfacturaventa=" + venta.Id) %>'">Descargar</button>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>

    <!-- Modal para Detalle de Factura -->
    <div id="transaccionesModal" class="transacciones-modal">
        <div class="transacciones-modal-content">
            <div class="transacciones-modal-header">
                <h5 class="transacciones-modal-title">Factura de la Transacción</h5>
                <span class="transacciones-close" onclick="cerrarModal()">&times;</span>
            </div>
            <div class="transacciones-modal-body">
                <div class="factura-info">
                    <p><strong>Fecha:</strong> <span id="facturaFecha"></span></p>
                    <p><strong>Factura N°:</strong> <span id="facturaNumero"></span></p>
                </div>

                <div id="accionesSection" style="display: none;">
                    <h3 class="section-title">Acciones</h3>
                    <table id="tablaAcciones">
                        <thead>
                            <tr>
                                <th>Nombre de Activo</th>
                                <th>Símbolo</th>
                                <th>Cantidad</th>
                                <th>Precio</th>
                                <th>Total</th>
                            </tr>
                        </thead>
                        <tbody id="detalleAccionesContainer">
                        </tbody>
                    </table>
                </div>

                <div id="separator" style="display: none;" class="separator"></div>

                <div id="bonosSection" style="display: none;">
                    <h3 class="section-title">Bonos</h3>
                    <table id="tablaBonos">
                        <thead>
                            <tr>
                                <th>Nombre de Activo</th>
                                <th>Valor Nominal</th>
                                <th>Tasa de Interés</th>
                                <th>Cantidad</th>
                                <th>Precio</th>
                                <th>Total</th>
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
                            <td>$${detalle.Precio.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            <td>$${(detalle.Precio * detalle.Cantidad).toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
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
                            <td>$${(detalle.Precio * detalle.Cantidad).toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
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
