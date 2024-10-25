<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transacciones.aspx.cs" Inherits="MidMarket.UI.Transacciones" MasterPageFile="~/Site.Master" Title="Mis Transacciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-transacciones">
        <h2>Administración de Transacciones</h2>

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
                <% foreach (var compra in Compras)
                    { %>
                <tr>
                    <td><%= compra.Id %></td>
                    <td><%= compra.Fecha.ToString("dd/MM/yyyy") %></td>
                    <td>$<%= compra.Total.ToString("N2") %></td>
                    <td>
                        <button type="button" class="submit-btn-transacciones ver-detalle-btn" onclick="abrirModalDetalle('<%= compra.Id %>')">Ver Detalle</button>
                        <button type="button" class="submit-btn-transacciones descargar-btn">Descargar</button>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>

    <div id="transaccionesModal" class="transacciones-modal">
        <div class="transacciones-modal-content">
            <div class="transacciones-modal-header">
                <h5 class="transacciones-modal-title">Detalles de la Compra</h5>
                <span class="transacciones-close" onclick="cerrarModal()">&times;</span>
            </div>
            <div class="transacciones-modal-body">
                <div id="detalleActivosContainer"></div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var comprasData = <%= ViewState["ComprasJson"] %>;

        function abrirModalDetalle(compraId) {
            event.preventDefault();
            event.stopPropagation();

            var compra = comprasData.find(c => c.Id == compraId);
            if (!compra) return;

            var detalleActivosContainer = document.getElementById('detalleActivosContainer');
            detalleActivosContainer.innerHTML = ''; // Limpiar contenido previo

            compra.Detalle.forEach(detalle => {
                var esAccion = detalle.Activo.Id_Accion !== undefined && detalle.Activo.Id_Accion !== 0;
                var esBono = detalle.Activo.Id_Bono !== undefined && detalle.Activo.Id_Bono !== 0;

                // Definimos el texto dependiendo si es acción o bono
                var precioLabel = esAccion ? "Precio" : "Valor Nominal";
                var precioValorNominal = esAccion ? detalle.Activo.Precio : detalle.Activo.ValorNominal;
                var tipoActivo = esAccion ? 'Acción' : (esBono ? 'Bono' : 'N/A');

                // Mostramos la tasa de interés solo para bonos
                var tasaInteresHtml = esBono ? `<p><strong>- Tasa de Interés:</strong> ${detalle.Activo.TasaInteres !== undefined ? detalle.Activo.TasaInteres.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : 'N/A'}%</p>` : '';

                // Mostrar símbolo solo si es una acción
                var simbolo = esAccion && detalle.Activo.Simbolo ? `<p><strong>- Símbolo:</strong> ${detalle.Activo.Simbolo}</p>` : '';

                // Estructura HTML del detalle del activo
                var activoHtml = `
            <p><strong>- Nombre de Activo:</strong> ${detalle.Activo.Nombre || 'N/A'}</p>
            ${simbolo}  <!-- Mostrar símbolo solo si es acción -->
            <p><strong>- Tipo:</strong> ${tipoActivo}</p>
            <p><strong>- ${precioLabel}:</strong> $${(precioValorNominal !== undefined ? precioValorNominal.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : 'N/A')}</p>
            ${tasaInteresHtml}  <!-- Mostrar tasa de interés solo si es bono -->
            <p><strong>- Cantidad:</strong> ${detalle.Cantidad || 'N/A'}</p>
            <p><strong>- Total:</strong> $${(detalle.Precio !== undefined ? detalle.Precio.toLocaleString('es-AR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : 'N/A')}</p>
            <hr>
        `;

                detalleActivosContainer.innerHTML += activoHtml;
            });

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
