<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesasignarFamilias.aspx.cs" Inherits="MidMarket.UI.DesasignarFamilias" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="desasignar-familias-container" class="container">
        <form method="post">
            <h2>Eliminar Familia a Usuario</h2>

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
                    <option value="<%= cliente.Id %>" <%= cliente.Id == UsuarioSeleccionadoId ? "selected" : "" %>>
                        <%= cliente.RazonSocial %>
                    </option>
                    <% } %>
                </select>
            </div>

            <h3>Familias Asignadas</h3>
            <table id="tablaFamiliasAsignadas">
                <thead>
                    <tr>
                        <th>Seleccionar</th>
                        <th>Nombre de Familia</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var familia in FamiliasAsignadas)
                        { %>
                    <tr>
                        <td>
                            <input type="checkbox" class="select-familia" value="<%= familia.Id %>">
                        </td>
                        <td><%= familia.Nombre %></td>
                        <td><%= familia.Permiso %></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>

            <input type="hidden" id="familiasSeleccionadas" name="familiasSeleccionadas">

            <asp:Button ID="btnEliminar" runat="server" Text="Desasignar Familias" OnClientClick="return prepararEnvio();" OnClick="btnEliminar_Click" CssClass="submit-btn" />
        </form>
    </div>

    <script>
        function prepararEnvio() {
            const familiasIds = [];
            document.querySelectorAll('.select-familia:checked').forEach(input => {
                familiasIds.push(input.value);
            });

            document.getElementById('familiasSeleccionadas').value = familiasIds.join(',');

            return familiasIds.length > 0;
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
