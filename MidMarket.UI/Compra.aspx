<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="MidMarket.UI.Compra" MasterPageFile="~/Site.Master" Title="Compra de Acciones y Bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="compra-container">
        <h1 class="compra-title">Acciones</h1>
        <div class="compra-grid">
            <asp:Repeater ID="rptAcciones" runat="server">
                <ItemTemplate>
                    <div class="compra-card compra-card-acciones">
                        <h3><%# Eval("Nombre") %></h3>
                        <div class="separator"></div>
                        <p>Símbolo: <%# Eval("Simbolo") %></p>
                        <p>Precio: $<%# Eval("Precio") %></p>

                        <asp:Button runat="server" Text="Agregar al Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="AgregarAccionAlCarrito_Click" CssClass="compra-button" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <h1 class="compra-title">Bonos</h1>
        <div class="compra-grid">
            <asp:Repeater ID="rptBonos" runat="server">
                <ItemTemplate>
                    <div class="compra-card compra-card-bonos">
                        <h3><%# Eval("Nombre") %></h3>
                        <div class="separator"></div>
                        <p>Valor Nominal: $<%# Eval("ValorNominal") %></p>
                        <p>Tasa de Interés: <%# Eval("TasaInteres") %>%</p>

                        <asp:Button runat="server" Text="Agregar al Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="AgregarBonoAlCarrito_Click" CssClass="compra-button" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
