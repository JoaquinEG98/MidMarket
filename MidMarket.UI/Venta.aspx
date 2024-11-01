<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="MidMarket.UI.Venta" MasterPageFile="~/Site.Master" Title="Ventas" %>

<%@ Import Namespace="MidMarket.Entities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="~/Content/Ventas.css" rel="stylesheet" />

    <div class="container-ventas">
        <h2 class="ventas-title">Ventas</h2>

        <asp:Repeater ID="rptTransacciones" runat="server">
            <HeaderTemplate>
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
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Repeater ID="rptDetalles" runat="server" DataSource='<%# Eval("Detalle") %>'>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Activo.Nombre") %></td>
                            <td><%# Eval("Cantidad") %></td>

                            <%-- Condicional para mostrar precios según el tipo de activo --%>
                            <td><%# Eval("Activo") is Accion ? ((Accion)Eval("Activo")).Precio.ToString("N2") : "-" %></td>
                            <td><%# Eval("Activo") is Bono ? ((Bono)Eval("Activo")).ValorNominal.ToString("N2") : "-" %></td>
                            <td>
                                <%# 
        Eval("Activo") is Accion ? 
        (Convert.ToInt32(Eval("Cantidad")) * ((Accion)Eval("Activo")).Precio).ToString("N2") : 
        (Eval("Activo") is Bono ? (Convert.ToInt32(Eval("Cantidad")) * ((Bono)Eval("Activo")).ValorNominal).ToString("N2") : "-") 
    %>
</td>
                            <td>
                                <asp:TextBox ID="txtCantidadVender" runat="server" Text="1" Width="50" />
                            </td>
                            <td>
                                <asp:Button ID="btnVender"
                                    runat="server"
                                    CssClass="btn btn-vender"
                                    CommandArgument='<%# Eval("Activo.Id") %>'
                                    Text="Vender"
                                    OnClick="VenderActivo_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
