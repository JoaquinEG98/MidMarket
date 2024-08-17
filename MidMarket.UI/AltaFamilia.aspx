<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaFamilia.aspx.cs" Inherits="MidMarket.UI.AltaFamilia" MasterPageFile="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="alta-familia-container" class="container">
        <form class="registro-form" method="post">
            <h2>Alta de Familia</h2>
            
            <div class="form-group">
                <h3>Nombre de Familia</h3>
                <input type="text" id="nombreFamilia" name="nombreFamilia" required>
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
                    <% foreach (var patente in Patentes) { %>
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

            <h3>Patentes Agregadas</h3>
            <table id="tablaAgregadas">
                <thead>
                    <tr>
                        <th>Nombre de Patente</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>

            <!-- Input hidden para almacenar los IDs seleccionados -->
            <input type="hidden" id="patentesSeleccionadas" name="patentesSeleccionadas">

            <!-- Botón ASP.NET con evento OnClick para el code-behind -->
            <asp:Button ID="btnCrear" runat="server" Text="Crear" OnClientClick="return prepararEnvio();" OnClick="btnCrear_Click" CssClass="submit-btn" />
        </form>
    </div>

    <script>
        function agregarPatente() {
            const checkboxes = document.querySelectorAll('.select-patente:checked');
            const tablaAgregadas = document.getElementById('tablaAgregadas').getElementsByTagName('tbody')[0];

            checkboxes.forEach(checkbox => {
                const fila = checkbox.parentElement.parentElement;
                const nuevaFila = fila.cloneNode(true);

                // Eliminar el checkbox y mover el input hidden con el ID
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

            // Asignar los IDs al input hidden
            document.getElementById('patentesSeleccionadas').value = patentesIds.join(',');

            // Si hay IDs, permite el envío del formulario
            return patentesIds.length > 0;
        }
    </script>
</asp:Content>