<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarFamilia.aspx.cs" Inherits="MidMarket.UI.ModificarFamilia" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="modificar-familia-container" class="container">
        <form class="registro-form">
            <h2>Modificar Familia</h2>

            <div class="form-group">
                <label for="nombreFamilia">Nombre de la Familia</label>
                <input type="text" id="nombreFamilia" name="nombreFamilia" value="Familia Ejemplo" required>
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
                    <tr>
                        <td>
                            <input type="checkbox" class="select-patente"></td>
                        <td>Patente 3</td>
                        <td>Descripción de la patente 3</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="select-patente"></td>
                        <td>Patente 4</td>
                        <td>Descripción de la patente 4</td>
                    </tr>
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
                    <tr>
                        <td>Patente 1</td>
                        <td>Descripción de la patente 1</td>
                    </tr>
                    <tr>
                        <td>Patente 2</td>
                        <td>Descripción de la patente 2</td>
                    </tr>
                </tbody>
            </table>

            <button type="button" class="submit-btn" onclick="guardar()">Guardar</button>
        </form>
    </div>

    <script>
        function agregarPatente() {
            const checkboxes = document.querySelectorAll('.select-patente:checked');
            const tablaAgregadas = document.getElementById('tablaAgregadas').getElementsByTagName('tbody')[0];

            checkboxes.forEach(checkbox => {
                const fila = checkbox.parentElement.parentElement;
                const nuevaFila = fila.cloneNode(true);

                nuevaFila.deleteCell(0);

                tablaAgregadas.appendChild(nuevaFila);

                checkbox.checked = false;
                checkbox.disabled = true;
            });
        }

        function guardar() {
            const nombreFamilia = document.getElementById('nombreFamilia').value;
            const tablaAgregadas = document.getElementById('tablaAgregadas').getElementsByTagName('tbody')[0];

            if (nombreFamilia.trim() === "") {
                alert("Por favor, ingresa el nombre de la familia.");
                return;
            }

            if (tablaAgregadas.rows.length > 0) {
                alert(`¡La familia "${nombreFamilia}" ha sido guardada con éxito!`);
            } else {
                alert("No hay patentes asignadas.");
            }
        }
    </script>
</asp:Content>
