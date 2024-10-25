<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transacciones.aspx.cs" Inherits="MidMarket.UI.Transacciones" MasterPageFile="~/Site.Master" Title="Administración de Transacciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-transacciones">
        <h2>Administración de Transacciones</h2>

        <div class="seccion-compra">
            <h3>Compras</h3>
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
                    <tr>
                        <td>001</td>
                        <td>25/10/2024</td>
                        <td>$1500</td>
                        <td>
                            <button type="button" class="submit-btn-transacciones ver-detalle-btn" onclick="abrirModalDetalle('001')">Ver Detalle</button>
                            <button type="button" class="submit-btn-transacciones descargar-btn">Descargar</button>
                        </td>
                    </tr>
                    <tr>
                        <td>002</td>
                        <td>18/10/2024</td>
                        <td>$2750</td>
                        <td>
                            <button type="button" class="submit-btn-transacciones ver-detalle-btn" onclick="abrirModalDetalle('002')">Ver Detalle</button>
                            <button type="button" class="submit-btn-transacciones descargar-btn">Descargar</button>
                        </td>
                    </tr>
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
                    <p><strong>- Nombre de Activo:</strong> Acciones XYZ</p>
                    <p><strong>- Cantidad:</strong> 100</p>
                    <p><strong>- Precio:</strong> $15 por unidad</p>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            function abrirModalDetalle(compraId) {
                event.preventDefault();
                event.stopPropagation();
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
    </div>
</asp:Content>
