<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transacciones.aspx.cs" Inherits="MidMarket.UI.Transacciones" MasterPageFile="~/Site.Master" Title="Administración de Transacciones" %>

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
                    <td>$<%= compra.Total %></td>
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
                <p><strong>- Nombre de Activo:</strong> <span id="detalleNombreActivo"></span></p>
                <p><strong>- Tipo:</strong> <span id="detalleTipoActivo"></span></p>
                <p><strong>- Precio/Valor Nominal:</strong> $<span id="detallePrecioValorNominal"></span></p>
                <p><strong>- Tasa de Interés:</strong> <span id="detalleTasaInteres"></span>%</p>
                <p><strong>- Cantidad:</strong> <span id="detalleCantidad"></span></p>
                <p><strong>- Total:</strong> $<span id="detalleTotal"></span></p>
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

            var detalle = compra.Detalle[0]; // Usamos el primer detalle como ejemplo

            document.getElementById('detalleNombreActivo').textContent = detalle.Activo.Nombre;
            document.getElementById('detalleTipoActivo').textContent = detalle.Activo.Id_Accion ? 'Accion' : 'Bono';
            document.getElementById('detallePrecioValorNominal').textContent = detalle.Activo.Precio || detalle.Activo.ValorNominal;
            document.getElementById('detalleTasaInteres').textContent = detalle.Activo.TasaInteres || 'N/A';
            document.getElementById('detalleCantidad').textContent = detalle.Cantidad;
            document.getElementById('detalleTotal').textContent = detalle.Precio;

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