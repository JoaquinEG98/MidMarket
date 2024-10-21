<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="MidMarket.UI.Usuarios" MasterPageFile="~/Site.Master" Title="Administración de Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-usuarios">
        <h2>Administración de Usuarios</h2>

        <table id="tablaUsuarios">
            <thead>
                <tr>
                    <th>Razón Social</th>
                    <th>CUIT</th>
                    <th>Familia</th>
                    <th>Ver Más</th> <!-- Botón para ver más -->
                    <th>Modificar</th> <!-- Botón para modificar -->
                </tr>
            </thead>
            <tbody>
                <% foreach (var cliente in Clientes) { %>
                <tr>
                    <td><%= cliente.RazonSocial %></td>
                    <td><%= cliente.CUIT %></td>
                    <td><%= cliente.Permisos[0].Nombre %></td>
                    <td>
                        <!-- Botón para ver detalles y abrir modal -->
                        <button type="button" class="submit-btn-usuarios ver-detalle-btn" onclick="abrirModalDetalle('<%= cliente.Id %>')">Ver Más</button>
                    </td>
                    <td>
                        <!-- Botón para modificar, redirige a la página de modificación -->
                        <button type="button" class="submit-btn-usuarios modificar-btn" onclick="window.location.href='<%= ResolveUrl("~/ModificarUsuario.aspx?id=" + cliente.Id) %>';">Modificar</button>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>

    <!-- Modal para ver detalles del usuario -->
    <!-- Modal para ver detalles del usuario -->
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

                <!-- Recuadro de seguridad para Familia y Patentes -->
                <div class="seguridad-container">
                    <p><strong>- Familia:</strong> <span id="detalleFamilia"></span></p>
                    <p><strong>- Patentes Asignadas:</strong>
                        <ul id="detallePatentes"></ul>
                    </p>
                </div>
            </div>
        </div>
    </div>

    <!-- Script para manejar el modal y redirecciones -->
    <script type="text/javascript">
        // Usamos JSON de ViewState para obtener los datos de los clientes
        var clientesData = <%= ViewState["ClientesJson"] %>;

        // Abre el modal y muestra los detalles del usuario seleccionado
        function abrirModalDetalle(clienteId) {
            // Detenemos el evento por si existe un conflicto
            event.preventDefault();
            event.stopPropagation();

            // Buscamos el cliente por ID en los datos obtenidos
            var cliente = clientesData.find(c => c.Id == clienteId);
            if (!cliente) return;

            // Seteamos los detalles del modal con la información del cliente
            document.getElementById('detalleId').textContent = cliente.Id;
            document.getElementById('detalleRazonSocial').textContent = cliente.RazonSocial;
            document.getElementById('detalleCuit').textContent = cliente.CUIT;
            document.getElementById('detalleNumeroCuenta').textContent = cliente.Cuenta.NumeroCuenta;
            document.getElementById('detalleSaldo').textContent = cliente.Cuenta.Saldo;
            document.getElementById('detalleFamilia').textContent = cliente.Permisos[0].Nombre;

            // Limpia y añade las patentes asignadas al usuario
            var listaPatentes = document.getElementById('detallePatentes');
            listaPatentes.innerHTML = ''; // Limpiamos la lista
            cliente.Permisos[0].Hijos.forEach(function (patente) {
                var li = document.createElement('li');
                li.textContent = patente.Nombre;
                listaPatentes.appendChild(li);
            });

            // Mostramos el modal
            document.getElementById('usuariosModal').style.display = 'block';
        }

        // Cierra el modal
        function cerrarModal() {
            document.getElementById('usuariosModal').style.display = 'none';
        }

        // Cierra el modal al hacer clic fuera de él
        window.onclick = function (event) {
            var modal = document.getElementById('usuariosModal');
            if (event.target == modal) {
                cerrarModal();
            }
        }
    </script>
</asp:Content>
