<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaFamilia.aspx.cs" Inherits="MidMarket.UI.AltaFamilia" MasterPageFile="~/Site.Master" Title="Alta de Familia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="alta-familia-container" class="container">
        <form class="registro-form" method="post">
            <h2 data-etiqueta="titulo_AltaFamilia">Alta de Familia</h2>

            <div class="form-group">
                <h3 data-etiqueta="label_NombreFamilia">Nombre de Familia</h3>
                <input type="text" id="nombreFamilia" name="nombreFamilia" required>
            </div>

            <h3 data-etiqueta="titulo_PatentesExistentes">Patentes Existentes</h3>
            <table id="tablaExistentes">
                <thead>
                    <tr>
                        <th data-etiqueta="table_Seleccionar">Seleccionar</th>
                        <th data-etiqueta="table_NombreFamilia">Nombre de Patente</th>
                        <th data-etiqueta="table_Descripcion">Descripción</th>
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

            <button type="button" class="submit-btn" onclick="agregarPatente()" data-etiqueta="btn_AgregarPatente">Agregar Patente</button>

            <h3 data-etiqueta="titulo_PatentesAgregadas">Patentes Agregadas</h3>
            <table id="tablaAgregadas">
                <thead>
                    <tr>
                        <th data-etiqueta="table_NombreFamilia">Nombre de Patente</th>
                        <th data-etiqueta="table_Descripcion">Descripción</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>

            <input type="hidden" id="patentesSeleccionadas" name="patentesSeleccionadas">

            <asp:Button ID="btnCrear" runat="server" Text="Crear" OnClientClick="return prepararEnvio();" OnClick="btnCrear_Click" CssClass="submit-btn" data-etiqueta="btn_Crear"/>
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
