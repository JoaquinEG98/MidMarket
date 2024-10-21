<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrarBonos.aspx.cs" Inherits="MidMarket.UI.AdministrarBonos" MasterPageFile="~/Site.Master" Title="Administrar Bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-bonos">
        <h2>Administración de Bonos</h2>

        <table id="tablaBonos">
            <thead>
                <tr>
                    <th>Seleccionar</th>
                    <th>Nombre de Bono</th>
                    <th>Valor Nominal</th>
                    <th>Tasa de Interés</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var bono in Bonos)
                    { %>
                <tr>
                    <td>
                        <input type="radio" name="selectBono" class="select-bono">
                        <input type="hidden" class="id-bono" value="<%= bono.Id %>">
                    </td>
                    <td><%= bono.Nombre %></td>
                    <td><%= bono.ValorNominal.ToString("F2") %></td>
                    <td><%= bono.TasaInteres.ToString("F2") %></td>
                </tr>
                <% } %>
            </tbody>
        </table>

        <div class="button-group">
            <button type="button" class="submit-btn-bonos" onclick="eliminarBono()">Baja</button>
            <button type="button" class="submit-btn-bonos" onclick="modificarBono()">Modificar</button>
            <button type="button" class="submit-btn-bonos" onclick="agregarBono()">Alta</button>
        </div>
    </div>

    <script>
        function modificarBono() {
            const seleccionado = document.querySelector('input[name="selectBono"]:checked');
            if (seleccionado) {
                const idBono = seleccionado.nextElementSibling.value;
                window.location.href = `ModificarBono.aspx?id=${idBono}`;
            } else {
                alert("Por favor, selecciona un bono para modificar.");
            }
        }

        function eliminarBono() {
            const seleccionado = document.querySelector('input[name="selectBono"]:checked');
            if (seleccionado) {
                const nombreBono = seleccionado.parentElement.nextElementSibling.textContent;
                const confirmar = confirm(`¿Estás seguro de eliminar el bono "${nombreBono}"?`);
                if (confirmar) {
                    alert(`El bono "${nombreBono}" ha sido eliminado.`);
                }
            } else {
                alert("Por favor, selecciona un bono para eliminar.");
            }
        }

        function agregarBono() {
            window.location.href = 'AltaBonos.aspx';
        }
    </script>
</asp:Content>
