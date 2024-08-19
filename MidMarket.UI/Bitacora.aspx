<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="MidMarket.UI.Bitacora" MasterPageFile="~/Site.Master" Title="Consulta de Bitácora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="bitacora-container" class="container">
        <h2>Consulta de Bitácora</h2>

        <div class="filter-container">
            <div class="form-group">
                <label for="filterUsuario">Filtrar por Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="filter-select">
                    <asp:ListItem Value="1">Usuario 1</asp:ListItem>
                    <asp:ListItem Value="2">Usuario 2</asp:ListItem>
                    <asp:ListItem Value="3">Usuario 3</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="filterCriticidad">Filtrar por Criticidad:</label>
                <asp:DropDownList ID="ddlCriticidad" runat="server" CssClass="filter-select">
                    <asp:ListItem Value="Alta">Alta</asp:ListItem>
                    <asp:ListItem Value="Media">Media</asp:ListItem>
                    <asp:ListItem Value="Baja">Baja</asp:ListItem>
                </asp:DropDownList>
            </div>

            <button type="button" class="submit-btn">Filtrar</button>
        </div>

        <table id="tablaBitacora">
            <thead>
                <tr>
                    <th>Fecha</th>
                    <th>Usuario</th>
                    <th>Criticidad</th>
                    <th>Mensaje</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>19/08/2024 10:00</td>
                    <td>Usuario 1</td>
                    <td>Alta</td>
                    <td>Error crítico en el sistema</td>
                </tr>
                <tr>
                    <td>18/08/2024 15:30</td>
                    <td>Usuario 2</td>
                    <td>Media</td>
                    <td>Proceso finalizado con advertencias</td>
                </tr>
                <tr>
                    <td>17/08/2024 12:45</td>
                    <td>Usuario 3</td>
                    <td>Baja</td>
                    <td>Operación completada sin problemas</td>
                </tr>
            </tbody>
        </table>

        <div class="pagination">
            <button type="button" class="submit-btn">Anterior</button>
            <span id="paginaActual">Página 1 de 10</span>
            <button type="button" class="submit-btn">Siguiente</button>
        </div>
    </div>
</asp:Content>
