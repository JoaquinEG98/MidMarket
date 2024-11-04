<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministracionFamilias.aspx.cs" Inherits="MidMarket.UI.AdministracionFamilias" MasterPageFile="~/Site.Master" Title="Administración de Familias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="familias-container" class="container">
        <h2 data-etiqueta="titulo_AdministracionFamilias">Administración de Familias</h2>

        <table id="tablaFamilias">
            <thead>
                <tr>
                    <th data-etiqueta="table_Seleccionar">Seleccionar</th>
                    <th data-etiqueta="table_NombreFamilia">Nombre de Familia</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var familia in Familias)
                    { %>
                <tr>
                    <td>
                        <input type="radio" name="selectFamilia" class="select-patente">
                        <input type="hidden" class="id-familia" value="<%= familia.Id %>">
                    </td>
                    <td><%= familia.Nombre %></td>
                </tr>
                <% } %>
            </tbody>
        </table>

        <div class="button-group">
            <button type="button" class="submit-btn" onclick="darBaja()" data-etiqueta="btn_Baja">Baja</button>
            <button type="button" class="submit-btn" onclick="modificarFamilia()" data-etiqueta="btn_Modificar">Modificar</button>
            <button type="button" class="submit-btn" onclick="altaFamilia()" data-etiqueta="btn_Alta">Alta</button>
        </div>
    </div>

    <script>
        function darBaja() {
            const seleccionada = document.querySelector('input[name="selectFamilia"]:checked');
            if (seleccionada) {
                const nombreFamilia = seleccionada.parentElement.nextElementSibling.textContent;
                const confirmar = confirm(`¿Estás seguro de dar de baja la familia "${nombreFamilia}"?`);
                if (confirmar) {
                    seleccionada.parentElement.parentElement.remove();
                    alert(`La familia "${nombreFamilia}" ha sido dada de baja.`);
                }
            } else {
                alert("Por favor, selecciona una familia para dar de baja.");
            }
        }

        function modificarFamilia() {
            const seleccionada = document.querySelector('input[name="selectFamilia"]:checked');
            if (seleccionada) {
                const idFamilia = seleccionada.nextElementSibling.value;
                window.location.href = `ModificarFamilia.aspx?id=${idFamilia}`;
            } else {
                alert("Por favor, selecciona una familia para modificar.");
            }
        }

        function altaFamilia() {
            window.location.href = 'AltaFamilia.aspx';
        }
    </script>
</asp:Content>
