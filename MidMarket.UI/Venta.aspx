<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="MidMarket.UI.Venta" MasterPageFile="~/Site.Master" Title="Ventas" %>
<%@ Import Namespace="MidMarket.Entities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="~/Content/Ventas.css" rel="stylesheet" />

    <div class="container-ventas">
        <h2 class="ventas-title">Ventas</h2>

        <table id="tablaVentas">
            <thead>
                <tr>
                    <th>Activo</th>
                    <th>Cantidad</th>
                    <th>Último Precio</th>
                    <th>Valor Nominal</th>
                    <th>Rendimiento</th>
                    <th>Cantidad a Vender</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var compra in Compras)
                   { %>
                   <% foreach (var detalle in compra.Detalle)
                      { %>
                    <tr>
                        <td><%= detalle.Activo.Nombre %></td>
                        <td><%= detalle.Cantidad %></td>

                        <% if (detalle.Activo is Accion accion)
                           { %>
                            <td><%= accion.Precio.ToString("C") %></td>
                            <td>-</td>
                            <td><%= (detalle.Cantidad * accion.Precio).ToString("C") %></td>
                        <% }
                           else if (detalle.Activo is Bono bono)
                           { %>
                            <td>-</td>
                            <td><%= bono.ValorNominal.ToString("C") %></td>
                            <td><%= (detalle.Cantidad * bono.ValorNominal).ToString("C") %></td>
                        <% } %>

                        <td><input type="number" class="cantidad-vender" value="1" min="1" max="<%= detalle.Cantidad %>"></td>
                        <td>
                            <button class="btn btn-vender" data-activo-id="<%= detalle.Activo.Id %>" onclick="venderActivo(this)">Vender</button>
                        </td>
                    </tr>
                   <% } %>
                <% } %>
            </tbody>
        </table>
    </div>
</asp:Content>
