<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaFamilia.aspx.cs" Inherits="MidMarket.UI.AltaFamilia" MasterPageFile="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="alta-familia-container" class="container">
        <form class="registro-form">
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

            <button type="button" class="submit-btn" onclick="crear()">Crear</button>
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

        function crear() {
            const nombreFamilia = document.getElementById('nombreFamilia').value;
            const tablaAgregadas = document.getElementById('tablaAgregadas').getElementsByTagName('tbody')[0];

            if (nombreFamilia.trim() === "") {
                alert("Por favor, ingresa el nombre de la familia.");
                return;
            }

            if (tablaAgregadas.rows.length > 0) {
                // Recopilar los IDs de las patentes agregadas
                const patentesIds = [];
                tablaAgregadas.querySelectorAll('.id-patente').forEach(input => {
                    patentesIds.push(input.value);
                });

                alert(`¡Las patentes de la familia "${nombreFamilia}" han sido creadas con éxito! IDs de las patentes: ${patentesIds.join(', ')}`);
            } else {
                alert("No hay patentes agregadas.");
            }
        }
    </script>
</asp:Content>