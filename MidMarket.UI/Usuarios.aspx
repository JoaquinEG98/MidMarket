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
                <% foreach (var cliente in Clientes) { %>
                <tr>
                    <td><%= cliente.RazonSocial %></td>
                    <td><%= cliente.CUIT %></td>
                    <td><%= cliente.Permisos[0].Nombre %></td>
                    <td>
                        <button class="submit-btn-usuarios ver-detalle-btn" onclick="abrirModalDetalle('<%= cliente.Id %>', event)">Ver Más</button>
                    </td>
                </tr>
                <% } %>
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
                <p><strong>- ID:</strong> <span id="detalleId"></span></p>
                <p><strong>- Razón Social:</strong> <span id="detalleRazonSocial"></span></p>
                <p><strong>- CUIT:</strong> <span id="detalleCuit"></span></p>
                <p><strong>- Número de Cuenta:</strong> <span id="detalleNumeroCuenta"></span></p>
                <p><strong>- Saldo:</strong> $<span id="detalleSaldo"></span></p>
                <div class="seguridad-container">
                    <p><strong>- Familia:</strong> <span id="detalleFamilia"></span></p>
                    <p><strong>- Patentes Asignadas:</strong>
                        <ul id="detallePatentes"></ul>
                    </p>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var clientesData = <%= ViewState["ClientesJson"] %>;

        function abrirModalDetalle(clienteId, event) {
            event.preventDefault();
            event.stopPropagation();

            var cliente = clientesData.find(c => c.Id == clienteId);
            if (!cliente) return;

            document.getElementById('detalleId').textContent = cliente.Id;
            document.getElementById('detalleRazonSocial').textContent = cliente.RazonSocial;
            document.getElementById('detalleCuit').textContent = cliente.CUIT;
            document.getElementById('detalleNumeroCuenta').textContent = cliente.Cuenta.NumeroCuenta;
            document.getElementById('detalleSaldo').textContent = cliente.Cuenta.Saldo;
            document.getElementById('detalleFamilia').textContent = cliente.Permisos[0].Nombre;

            var listaPatentes = document.getElementById('detallePatentes');
            listaPatentes.innerHTML = ''; 
            cliente.Permisos[0].Hijos.forEach(function (patente) {
                var li = document.createElement('li');
                li.textContent = patente.Nombre;
                listaPatentes.appendChild(li);
            });

            document.getElementById('usuariosModal').style.display = 'block';
        }

        function cerrarModal() {
            document.getElementById('usuariosModal').style.display = 'none';
        }

        window.onclick = function (event) {
            var modal = document.getElementById('usuariosModal');
            if (event.target == modal) {
                cerrarModal();
            }
        }
    </script>
</asp:Content>