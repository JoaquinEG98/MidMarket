<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrarAcciones.aspx.cs" Inherits="MidMarket.UI.AdministrarAcciones" MasterPageFile="~/Site.Master" Title="Administración de Acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="acciones-container" class="container-acciones">
        <h2>Administración de Acciones</h2>

        <table id="tablaAcciones">
            <thead>
                <tr>
                    <th>Seleccionar</th>
                    <th>Nombre de Acción</th>
                    <th>Símbolo</th>
                    <th>Precio</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var accion in Acciones) { %>
                <tr>
                    <td>
                        <input type="radio" name="selectAccion" class="select-accion">
                        <input type="hidden" class="id-accion" value="<%= accion.Id %>">
                    </td>
                    <td><%= accion.Nombre %></td>
                    <td><%= accion.Simbolo %></td>
                    <td><%= accion.Precio %></td>
                </tr>
                <% } %>
            </tbody>
        </table>

        <div class="button-group">
            <button type="button" class="submit-btn-acciones" onclick="darBajaAccion()">Baja</button>
            <button type="button" class="submit-btn-acciones" onclick="modificarAccion()">Modificar</button>
            <button type="button" class="submit-btn-acciones" onclick="altaAccion()">Alta</button>
        </div>
    </div>

    <script>
        function darBajaAccion() {
            const seleccionada = document.querySelector('input[name="selectAccion"]:checked');
            if (seleccionada) {
                const nombreAccion = seleccionada.parentElement.nextElementSibling.textContent;
                const confirmar = confirm(`¿Estás seguro de dar de baja la acción "${nombreAccion}"?`);
                if (confirmar) {
                    seleccionada.parentElement.parentElement.remove();
                    alert(`La acción "${nombreAccion}" ha sido dada de baja.`);
                }
            } else {
                alert("Por favor, selecciona una acción para dar de baja.");
            }
        }

        function modificarAccion() {
            const seleccionada = document.querySelector('input[name="selectAccion"]:checked');
            if (seleccionada) {
                const idAccion = seleccionada.nextElementSibling.value;
                window.location.href = `ModificarAccion.aspx?id=${idAccion}`;
            } else {
                alert("Por favor, selecciona una acción para modificar.");
            }
        }

        function altaAccion() {
            window.location.href = 'AltaAcciones.aspx';
        }
    </script>
</asp:Content>
