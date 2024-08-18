<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignarPatentes.aspx.cs" Inherits="MidMarket.UI.AsignarPatentes" MasterPageFile="~/Site.Master" Title="Asignar Patentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="asignar-patentes-container" class="container">
        <form method="post">
            <h2>Asignación de Patente a Usuario</h2>

            <div class="form-group">
                <label for="filtroUsuario">Filtrar Usuario:</label>
                <input type="text" id="filtroUsuario" name="filtroUsuario" value="<%= ViewState["FiltroUsuario"] ?? "" %>" oninput="filtrarUsuarios()" placeholder="Escribe para buscar...">
            </div>

            <div class="form-group">
                <label for="selectUsuario">Seleccionar Usuario:</label>
                <select id="selectUsuario" name="usuarioSeleccionado" onchange="this.form.submit()">
                    <option value="">Selecciona un usuario</option>
                    <% foreach (var cliente in Clientes)
                        { %>
                    <option value="<%= cliente.Id %>" <%= cliente.Id == ClienteSeleccionadoId ? "selected" : "" %>>
                        <%= cliente.RazonSocial %>
                    </option>
                    <% } %>
                </select>
            </div>

            <h3>Patentes Asignadas</h3>
            <table id="tablaPatentesAsignadas">
                <thead>
                    <tr>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var patente in PatentesAsignadas)
                        { %>
                    <tr>
                        <td><%= patente.Nombre %></td>
                        <td><%= patente.Permiso %></td>
                        <input type="hidden" class="id-patente-asignada" value="<%= patente.Id %>">
                    </tr>
                    <% } %>
                </tbody>
            </table>

            <h3>Patentes Disponibles</h3>
            <table id="tablaPatentesDisponibles">
                <thead>
                    <tr>
                        <th>Seleccionar</th>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var patente in PatentesDisponibles)
                        { %>
                    <tr>
                        <td>
                            <input type="checkbox" class="select-patente" value="<%= patente.Id %>">
                        </td>
                        <td><%= patente.Nombre %></td>
                        <td><%= patente.Permiso %></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>

            <input type="hidden" id="patentesSeleccionadas" name="patentesSeleccionadas">
            <input type="hidden" id="patentesAsignadas" name="patentesAsignadas">

            <asp:Button ID="btnGuardar" runat="server" Text="Asignar Patentes" OnClientClick="return prepararEnvio();" OnClick="btnGuardar_Click" CssClass="submit-btn" />
        </form>
    </div>

    <script>
        function prepararEnvio() {
            const patentesSeleccionadasIds = [];
            document.querySelectorAll('.select-patente:checked').forEach(input => {
                patentesSeleccionadasIds.push(input.value);
            });

            document.getElementById('patentesSeleccionadas').value = patentesSeleccionadasIds.join(',');

            const patentesAsignadasIds = [];
            document.querySelectorAll('.id-patente-asignada').forEach(input => {
                patentesAsignadasIds.push(input.value);
            });

            document.getElementById('patentesAsignadas').value = patentesAsignadasIds.join(',');

            return true;
        }

        function filtrarUsuarios() {
            const filtro = document.getElementById('filtroUsuario').value.toLowerCase();
            const selectUsuario = document.getElementById('selectUsuario');
            const opciones = selectUsuario.getElementsByTagName('option');

            for (let i = 1; i < opciones.length; i++) {
                const texto = opciones[i].textContent.toLowerCase();
                opciones[i].style.display = texto.includes(filtro) ? '' : 'none';
            }
        }
    </script>
</asp:Content>