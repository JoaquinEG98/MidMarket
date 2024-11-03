<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrarAcciones.aspx.cs" Inherits="MidMarket.UI.AdministrarAcciones" MasterPageFile="~/Site.Master" Title="Administración de Acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="acciones-container" class="container-acciones">
        <h2 data-etiqueta="titulo_AdministracionAcciones">Administración de Acciones</h2>

        <table id="tablaAcciones">
            <thead>
                <tr>
                    <th data-etiqueta="table_Seleccionar">Seleccionar</th>
                    <th data-etiqueta="table_NombreAccion">Nombre de Acción</th>
                    <th data-etiqueta="table_Simbolo">Símbolo</th>
                    <th data-etiqueta="table_Precio">Precio</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var accion in Acciones)
                    { %>
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
            <button type="button" class="submit-btn-acciones" onclick="darBajaAccion()" data-etiqueta="btn_Baja">Baja</button>
            <button type="button" class="submit-btn-acciones" onclick="modificarAccion()" data-etiqueta="btn_Modificar">Modificar</button>
            <button type="button" class="submit-btn-acciones" onclick="altaAccion()" data-etiqueta="btn_Alta">Alta</button>
        </div>
    </div>

    <script>
        function darBajaAccion() {
            const seleccionada = document.querySelector('input[name="selectAccion"]:checked');
            if (seleccionada) {
                const nombreAccion = seleccionada.parentElement.nextElementSibling.textContent;
                const confirmar = confirm(traducciones["confirmacion_BajaAccion"].replace("{NombreAccion}", nombreAccion));
                if (confirmar) {
                    seleccionada.parentElement.parentElement.remove();
                    alert(traducciones["alerta_AccionBaja"].replace("{NombreAccion}", nombreAccion));
                }
            } else {
                alert(traducciones["alerta_SeleccionarAccion"]);
            }
        }

        function modificarAccion() {
            const seleccionada = document.querySelector('input[name="selectAccion"]:checked');
            if (seleccionada) {
                const idAccion = seleccionada.nextElementSibling.value;
                window.location.href = `ModificarAccion.aspx?id=${idAccion}`;
            } else {
                alert(traducciones["alerta_SeleccionarAccion"]);
            }
        }

        function altaAccion() {
            window.location.href = 'AltaAcciones.aspx';
        }
    </script>
</asp:Content>
