<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="MidMarket.UI.Bitacora" MasterPageFile="~/Site.Master" Title="Consulta de Bitácora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="bitacora-container" class="container">
        <h2>Consulta de Bitácora</h2>

        <div class="filter-container">
            <div class="form-group">
                <label for="filterUsuario">Filtrar por Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="filter-select" AutoPostBack="true" OnSelectedIndexChanged="FiltrarBitacora">
                    <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                    <asp:ListItem Text="Usuario 1" Value="Usuario 1"></asp:ListItem>
                    <asp:ListItem Text="Usuario 2" Value="Usuario 2"></asp:ListItem>
                    <asp:ListItem Text="Usuario 3" Value="Usuario 3"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="filterCriticidad">Filtrar por Criticidad:</label>
                <asp:DropDownList ID="ddlCriticidad" runat="server" CssClass="filter-select" AutoPostBack="true" OnSelectedIndexChanged="FiltrarBitacora">
                    <asp:ListItem Text="Todas" Value=""></asp:ListItem>
                    <asp:ListItem Text="Baja" Value="Baja"></asp:ListItem>
                    <asp:ListItem Text="Media" Value="Media"></asp:ListItem>
                    <asp:ListItem Text="Alta" Value="Alta"></asp:ListItem>
                </asp:DropDownList>
            </div>
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
                <% foreach (var movimiento in Movimientos)
                    { %>
                <tr>
                    <td><%= movimiento.Fecha.ToString("dd/MM/yyyy HH:mm") %></td>
                    <td><%= movimiento.Cliente.RazonSocial %></td>
                    <td><%= movimiento.Criticidad.ToString() %></td>
                    <td><%= movimiento.Descripcion %></td>
                </tr>
                <% } %>
            </tbody>
        </table>

        <div class="pagination">
            <asp:Button ID="btnAnterior" runat="server" Text="Anterior" CssClass="submit-btn" OnClick="btnAnterior_Click" />
            <span id="paginaActual">Página <%# GetPaginaActual() %> de <%# GetTotalPaginas() %></span>
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="submit-btn" OnClick="btnSiguiente_Click" />
        </div>
    </div>
</asp:Content>
