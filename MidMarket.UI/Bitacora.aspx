<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="MidMarket.UI.Bitacora" MasterPageFile="~/Site.Master" Title="Consulta de Bitácora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="bitacora-container" class="container">
        <h2 data-etiqueta="title_LogConsulta">Consulta de Bitácora</h2>

        <div class="filter-container">
            <div class="form-group">
                <label for="filterUsuario" data-etiqueta="label_FilterUser">Filtrar por Usuario:</label>
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="filter-select" AutoPostBack="true" OnSelectedIndexChanged="ConsultarBitacoraFiltro">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="filterCriticidad" data-etiqueta="label_FilterSeverity">Filtrar por Criticidad:</label>
                <asp:DropDownList ID="ddlCriticidad" runat="server" CssClass="filter-select" AutoPostBack="true" OnSelectedIndexChanged="ConsultarBitacoraFiltro">
                    <asp:ListItem Text="Todas" Value="" data-etiqueta="option_All"></asp:ListItem>
                    <asp:ListItem Text="Baja" Value="Baja" data-etiqueta="option_Low"></asp:ListItem>
                    <asp:ListItem Text="Media" Value="Media" data-etiqueta="option_Medium"></asp:ListItem>
                    <asp:ListItem Text="Alta" Value="Alta" data-etiqueta="option_High"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="filterFechaDesde" data-etiqueta="label_From">Desde:</label>
                <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="filter-input" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="filterFechaHasta" data-etiqueta="label_To">Hasta:</label>
                <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="filter-input" TextMode="Date"></asp:TextBox>
            </div>

            <asp:Button ID="btnFiltrarFechas" runat="server" Text="Filtrar" CssClass="submit-btn" OnClick="ConsultarBitacoraFiltro" data-etiqueta="btn_Filter" />

            <div class="export-buttons">
                <asp:Button ID="btnExportarXML" runat="server" Text="Exportar a XML" CssClass="submit-btn" OnClick="ExportarXML_Click" data-etiqueta="btn_ExportToXML" />
                <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar a Excel" CssClass="submit-btn" OnClick="ExportarExcel_Click" data-etiqueta="btn_ExportToExcel" />
            </div>
        </div>

        <table id="tablaBitacora">
            <thead>
                <tr>
                    <th data-etiqueta="table_Date">Fecha</th>
                    <th data-etiqueta="table_User">Usuario</th>
                    <th data-etiqueta="table_Severity">Criticidad</th>
                    <th data-etiqueta="table_Message">Mensaje</th>
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
            <asp:Button ID="btnAnterior" runat="server" Text="Anterior" CssClass="submit-btn" OnClick="btnAnterior_Click" data-etiqueta="btn_Previous" />
            <span id="paginaActual">
                <span data-etiqueta="label_Page">Página</span> <%# GetPaginaActual() %> 
                <span data-etiqueta="label_TotalPaginas">de</span> <%# GetTotalPaginas() %>
            </span>
            <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" CssClass="submit-btn" OnClick="btnSiguiente_Click" data-etiqueta="btn_Next" />
        </div>
    </div>
</asp:Content>
