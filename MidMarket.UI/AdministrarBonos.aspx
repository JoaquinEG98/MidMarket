<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministrarBonos.aspx.cs" Inherits="MidMarket.UI.AdministrarBonos" MasterPageFile="~/Site.Master" Title="Administrar Bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-bonos">
        <h2 data-etiqueta="titulo_AdministracionBonos">Administración de Bonos</h2>

        <table id="tablaBonos">
            <thead>
                <tr>
                    <th data-etiqueta="table_Seleccionar">Seleccionar</th>
                    <th data-etiqueta="table_NombreBono">Nombre de Bono</th>
                    <th data-etiqueta="table_ValorNominal">Valor Nominal</th>
                    <th data-etiqueta="table_TasaInteres">Tasa de Interés</th>
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
            <button type="button" class="submit-btn-bonos" onclick="eliminarBono()" data-etiqueta="btn_BajaBono">Baja</button>
            <button type="button" class="submit-btn-bonos" onclick="modificarBono()" data-etiqueta="btn_ModificarBono">Modificar</button>
            <button type="button" class="submit-btn-bonos" onclick="agregarBono()" data-etiqueta="btn_AltaBono">Alta</button>
        </div>
    </div>

    <script>
        function modificarBono() {
            const seleccionado = document.querySelector('input[name="selectBono"]:checked');
            if (seleccionado) {
                const idBono = seleccionado.nextElementSibling.value;
                window.location.href = `ModificarBono.aspx?id=${idBono}`;
            } else {
                alert(traducciones["alert_SeleccionarBono"]);
            }
        }

        function eliminarBono() {
            const seleccionado = document.querySelector('input[name="selectBono"]:checked');
            if (seleccionado) {
                const nombreBono = seleccionado.parentElement.nextElementSibling.textContent;
                const confirmar = confirm(traducciones["confirm_EliminarBono"].replace("{NombreBono}", nombreBono));
                if (confirmar) {
                    alert(traducciones["alert_BonoEliminado"].replace("{NombreBono}", nombreBono));
                }
            } else {
                alert(traducciones["alert_SeleccionarBono"]);
            }
        }

        function agregarBono() {
            window.location.href = 'AltaBonos.aspx';
        }
    </script>
</asp:Content>
