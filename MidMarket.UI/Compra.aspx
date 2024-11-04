<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="MidMarket.UI.Compra" MasterPageFile="~/Site.Master" Title="Compra de Acciones y Bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="compra-container">
        <h1 class="compra-title" data-etiqueta="section_Acciones">Acciones</h1>
        <div class="compra-grid">
            <asp:Repeater ID="rptAcciones" runat="server">
                <ItemTemplate>
                    <div class="compra-card compra-card-acciones">
                        <h3><%# Eval("Nombre") %></h3>
                        <div class="separator"></div>
                        <p data-etiqueta="card_Simbolo" data-simbolo="<%# Eval("Simbolo") %>">Símbolo: <%# Eval("Simbolo") %></p>
                        <p data-etiqueta="card_Precio" data-precio="<%# Convert.ToDecimal(Eval("Precio")).ToString("N2") %>">Precio: $<%# Convert.ToDecimal(Eval("Precio")).ToString("N2") %></p>

                        <asp:Button runat="server" Text="Agregar al Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="AgregarAccionAlCarrito_Click" CssClass="compra-button" data-etiqueta="btn_AgregarCarritoAccion" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <h1 class="compra-title" data-etiqueta="section_Bonos">Bonos</h1>
        <div class="compra-grid">
            <asp:Repeater ID="rptBonos" runat="server">
                <ItemTemplate>
                    <div class="compra-card compra-card-bonos">
                        <h3><%# Eval("Nombre") %></h3>
                        <div class="separator"></div>
                        <p data-etiqueta="card_ValorNominal" data-valornominal="<%# Convert.ToDecimal(Eval("ValorNominal")).ToString("N2") %>">Valor Nominal: $<%# Convert.ToDecimal(Eval("ValorNominal")).ToString("N2") %></p>
                        <p data-etiqueta="card_TasaInteres" data-tasainteres="<%# Eval("TasaInteres") %>">Tasa de Interés: <%# Eval("TasaInteres") %>%</p>

                        <asp:Button runat="server" Text="Agregar al Carrito" CommandArgument='<%# Eval("Id") %>' OnClick="AgregarBonoAlCarrito_Click" CssClass="compra-button" data-etiqueta="btn_AgregarCarritoBono" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
