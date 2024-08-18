<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignarPatentes.aspx.cs" Inherits="MidMarket.UI.AsignarPatentes" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="asignar-patentes-container" class="container">
        <form method="post">
            <h2>Asignación de Patente a Usuario</h2>

            <div class="form-group">
                <label for="selectUsuario">Seleccionar Usuario:</label>
                <select id="selectUsuario" name="usuarioSeleccionado" onchange="this.form.submit()">
                    <option value="">Selecciona un usuario</option>
                    <% foreach (var cliente in Clientes)
                        { %>
                    <option value="<%= cliente.Id %>" <%= cliente.Id == UsuarioSeleccionadoId ? "selected" : "" %>>
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

            <!-- Inputs hidden para almacenar los IDs -->
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
    </script>
</asp:Content>