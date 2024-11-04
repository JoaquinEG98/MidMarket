<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignarFamilias.aspx.cs" Inherits="MidMarket.UI.AsignarFamilias" MasterPageFile="~/Site.Master" Title="Asignar Familias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="asignar-familias-container" class="container">
        <form method="post">
            <h2 data-etiqueta="titulo_AsignarFamilias">Asignación de Familia a Usuario</h2>

            <div class="form-group">
                <label for="filtroUsuario" data-etiqueta="label_FiltrarUsuario">Filtrar Usuario:</label>
                <input type="text" id="filtroUsuario" name="filtroUsuario" value="<%= ViewState["FiltroUsuario"] ?? "" %>" oninput="filtrarUsuarios()" placeholder="Escribe para buscar..." data-etiqueta="placeholder_FiltrarUsuario">
            </div>

            <div class="form-group">
                <label for="selectUsuario" data-etiqueta="label_SeleccionarUsuario">Seleccionar Usuario:</label>
                <select id="selectUsuario" name="usuarioSeleccionado" onchange="this.form.submit()">
                    <option value="" data-etiqueta="option_SeleccionarUsuario">Selecciona un usuario</option>
                    <% foreach (var cliente in Clientes)
                        { %>
                    <option value="<%= cliente.Id %>" <%= cliente.Id == UsuarioSeleccionadoId ? "selected" : "" %>>
                        <%= cliente.RazonSocial %>
                    </option>
                    <% } %>
                </select>
            </div>

            <h3 data-etiqueta="titulo_FamiliasAsignadas">Familias Asignadas</h3>
            <table id="tablaFamiliasAsignadas">
                <thead>
                    <tr>
                        <th data-etiqueta="table_NombreFamilia">Nombre de Familia</th>
                        <th data-etiqueta="table_DescripcionFamilia">Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var familia in FamiliasAsignadas)
                        { %>
                    <tr>
                        <td><%= familia.Nombre %></td>
                        <td><%= familia.Permiso %></td>
                        <input type="hidden" class="id-familia-asignada" value="<%= familia.Id %>">
                    </tr>
                    <% } %>
                </tbody>
            </table>

            <h3 data-etiqueta="titulo_FamiliasDisponibles">Familias Disponibles</h3>
            <table id="tablaFamiliasDisponibles">
                <thead>
                    <tr>
                        <th data-etiqueta="table_Seleccionar">Seleccionar</th>
                        <th data-etiqueta="table_NombreFamilia">Nombre de Familia</th>
                        <th data-etiqueta="table_DescripcionFamilia">Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var familia in FamiliasDisponibles)
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
            <input type="hidden" id="familiasAsignadas" name="familiasAsignadas">

            <asp:Button ID="btnGuardar" runat="server" Text="Asignar Familias" OnClientClick="return prepararEnvio();" OnClick="btnGuardar_Click" CssClass="submit-btn" data-etiqueta="btn_AsignarFamilias" />
        </form>
    </div>

    <script>
        function prepararEnvio() {
            const familiasSeleccionadasIds = [];
            document.querySelectorAll('.select-familia:checked').forEach(input => {
                familiasSeleccionadasIds.push(input.value);
            });

            document.getElementById('familiasSeleccionadas').value = familiasSeleccionadasIds.join(',');

            const familiasAsignadasIds = [];
            document.querySelectorAll('.id-familia-asignada').forEach(input => {
                familiasAsignadasIds.push(input.value);
            });

            document.getElementById('familiasAsignadas').value = familiasAsignadasIds.join(',');

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
