<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarFamilia.aspx.cs" Inherits="MidMarket.UI.ModificarFamilia" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="modificar-familia-container" class="container">
        <form class="registro-form" method="post">
            <h2>Modificar Familia</h2>

            <div class="form-group">
                <label for="nombreFamilia">Nombre de la Familia</label>
                <input type="text" id="nombreFamilia" name="nombreFamilia" value="<%= Familia.Nombre %>" required>
            </div>

            <h3>Patentes Existentes</h3>
            <table id="tablaExistentes">
                <thead>
                    <tr>
                        <th>Seleccionar</th>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var patente in PatentesExistentes)
                        { %>
                    <tr>
                        <td>
                            <input type="checkbox" class="select-patente">
                            <input type="hidden" class="id-patente" value="<%= patente.Id %>">
                        </td>
                        <td><%= patente.Nombre %></td>
                        <td><%= patente.Permiso %></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>

            <button type="button" class="submit-btn" onclick="agregarPatente()">Agregar Patente</button>

            <h3>Patentes Asignadas</h3>
            <table id="tablaAgregadas">
                <thead>
                    <tr>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var patente in Patentes)
                        { %>
                    <tr>
                        <td><%= patente.Nombre %></td>
                        <td><%= patente.Permiso %></td>
                        <input type="hidden" class="id-patente" value="<%= patente.Id %>">
                    </tr>
                    <% } %>
                </tbody>
            </table>

            <input type="hidden" id="patentesSeleccionadas" name="patentesSeleccionadas">

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClientClick="return prepararEnvio();" OnClick="btnGuardar_Click" CssClass="submit-btn" />
        </form>
    </div>

    <script>
        function agregarPatente() {
            const checkboxes = document.querySelectorAll('.select-patente:checked');
            const tablaAgregadas = document.getElementById('tablaAgregadas').getElementsByTagName('tbody')[0];

            checkboxes.forEach(checkbox => {
                const fila = checkbox.parentElement.parentElement;
                const nuevaFila = fila.cloneNode(true);

                const inputHidden = fila.querySelector('.id-patente').outerHTML;
                nuevaFila.deleteCell(0);
                nuevaFila.innerHTML += `<td style="display: none;">${inputHidden}</td>`;

                tablaAgregadas.appendChild(nuevaFila);

                checkbox.checked = false;
                checkbox.disabled = true;
            });
        }

        function prepararEnvio() {
            const patentesIds = [];
            document.querySelectorAll('#tablaAgregadas .id-patente').forEach(input => {
                patentesIds.push(input.value);
            });

            document.getElementById('patentesSeleccionadas').value = patentesIds.join(',');

            return patentesIds.length > 0;
        }
    </script>
</asp:Content>