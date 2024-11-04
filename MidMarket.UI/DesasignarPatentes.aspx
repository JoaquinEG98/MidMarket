<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesasignarPatentes.aspx.cs" Inherits="MidMarket.UI.DesasignarPatentes" MasterPageFile="~/Site.Master" Title="Desasignar Patentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="desasignar-patentes-container" class="container">
        <form method="post">
            <h2 data-etiqueta="titulo_DesasignarPatentes">Eliminar Patente a Usuario</h2>

            <div class="form-group">
                <label for="filtroUsuario" data-etiqueta="label_FiltrarUsuario">Filtrar Usuario:</label>
                <input type="text" id="filtroUsuario" name="filtroUsuario" value="<%= ViewState["FiltroUsuario"] ?? "" %>" oninput="filtrarUsuarios()" placeholder="Escribe para buscar..." data-etiqueta="placeholder_BuscarUsuario">
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

            <h3 data-etiqueta="titulo_PatentesAsignadas">Patentes Asignadas</h3>
            <table id="tablaPatentesAsignadas">
                <thead>
                    <tr>
                        <th data-etiqueta="table_Seleccionar">Seleccionar</th>
                        <th data-etiqueta="table_NombrePatente">Nombre de Patente</th>
                        <th data-etiqueta="table_Descripcion">Descripción</th>
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

            <asp:Button ID="btnEliminar" runat="server" Text="Desasignar Patentes" CssClass="submit-btn" data-etiqueta="btn_DesasignarPatentes" OnClientClick="return prepararEnvio();" OnClick="btnEliminar_Click" />
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
