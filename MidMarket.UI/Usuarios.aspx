<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="MidMarket.UI.Usuarios" MasterPageFile="~/Site.Master" Title="Administración de Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="usuarios-container" class="container-usuarios">
        <h2>Administración de Usuarios</h2>

        <table id="tablaUsuarios">
            <thead>
                <tr>
                    <th>Razón Social</th>
                    <th>CUIT</th>
                    <th>Familia</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Empresa 1 S.A.</td>
                    <td>30-12345678-9</td>
                    <td>Industria</td>
                    <td>
                        <button class="submit-btn-usuarios ver-detalle-btn" onclick="abrirModalDetalle(event, '1', 'Empresa 1 S.A.', '30-12345678-9', 'Industria', 'PatenteA, PatenteB')">Ver Más</button>
                    </td>
                </tr>
                <tr>
                    <td>Servicios Globales SRL</td>
                    <td>33-87654321-5</td>
                    <td>Servicios</td>
                    <td>
                        <button class="submit-btn-usuarios ver-detalle-btn" onclick="abrirModalDetalle(event, '2', 'Servicios Globales SRL', '33-87654321-5', 'Servicios', 'PatenteC, PatenteD')">Ver Más</button>
                    </td>
                </tr>
                <tr>
                    <td>Comercial ABC</td>
                    <td>30-98765432-1</td>
                    <td>Comercialización</td>
                    <td>
                        <button class="submit-btn-usuarios ver-detalle-btn" onclick="abrirModalDetalle(event, '3', 'Comercial ABC', '30-98765432-1', 'Comercialización', 'PatenteE, PatenteF')">Ver Más</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <div id="usuariosModal" class="usuarios-modal">
        <div class="usuarios-modal-content">
            <div class="usuarios-modal-header">
                <h5 class="usuarios-modal-title">Detalles del Usuario</h5>
                <span class="usuarios-close" onclick="cerrarModal()">&times;</span>
            </div>
            <div class="usuarios-modal-body">
                <p><strong>ID:</strong> <span id="detalleId"></span></p>
                <p><strong>Razón Social:</strong> <span id="detalleRazonSocial"></span></p>
                <p><strong>CUIT:</strong> <span id="detalleCuit"></span></p>
                <p><strong>Familia:</strong> <span id="detalleFamilia"></span></p>
                <p><strong>Patentes Asociadas:</strong> <span id="detallePatentes"></span></p>
            </div>
        </div>
    </div>

    <script>
        function abrirModalDetalle(event, id, razonSocial, cuit, familia, patentes) {
            event.preventDefault();

            document.getElementById('detalleId').textContent = id;
            document.getElementById('detalleRazonSocial').textContent = razonSocial;
            document.getElementById('detalleCuit').textContent = cuit;
            document.getElementById('detalleFamilia').textContent = familia;
            document.getElementById('detallePatentes').textContent = patentes;

            document.getElementById('usuariosModal').style.display = 'block';
        }

        function cerrarModal() {
            document.getElementById('usuariosModal').style.display = 'none';
        }

        window.onclick = function (event) {
            var modal = document.getElementById('usuariosModal');
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>
</asp:Content>