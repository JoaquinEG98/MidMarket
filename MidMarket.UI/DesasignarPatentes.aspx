<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesasignarPatentes.aspx.cs" Inherits="MidMarket.UI.DesasignarPatentes" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="desasignar-patentes-container" class="container">
        <form method="post">
            <h2>Eliminar Patente a Usuario</h2>

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

            <h3>Patentes Asignadas</h3>
            <table id="tablaPatentesAsignadas">
                <thead>
                    <tr>
                        <th>Seleccionar</th>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var patente in PatentesAsignadas)
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

            <asp:Button ID="btnEliminar" runat="server" Text="Desasignar Patentes" OnClientClick="return prepararEnvio();" OnClick="btnEliminar_Click" CssClass="submit-btn" />
        </form>
    </div>

    <script>
        function prepararEnvio() {
            const patentesIds = [];
            document.querySelectorAll('.select-patente:checked').forEach(input => {
                patentesIds.push(input.value);
            });

            document.getElementById('patentesSeleccionadas').value = patentesIds.join(',');

            return patentesIds.length > 0;
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
