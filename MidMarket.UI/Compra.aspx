<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="MidMarket.UI.Compra" MasterPageFile="~/Site.Master" Title="Compra de Acciones y Bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="compra-container">
        <h1 class="compra-title">Acciones</h1>
        <div class="compra-grid">
            <% foreach (var accion in Acciones) { %>
            <div class="compra-card compra-card-acciones">
                <h3><%= accion.Nombre %></h3>
                <div class="separator"></div>
                <p>Símbolo: <%= accion.Simbolo %></p>
                <p>Precio: $<%= accion.Precio %></p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
            <% } %>
        </div>

        <h1 class="compra-title">Bonos</h1>
        <div class="compra-grid">
            <% foreach (var bono in Bonos) { %>
            <div class="compra-card compra-card-bonos">
                <h3><%= bono.Nombre %></h3>
                <div class="separator"></div>
                <p>Valor Nominal: $<%= bono.ValorNominal %></p>
                <p>Tasa de Interés: <%= bono.TasaInteres %>%</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
            <% } %>
        </div>
    </div>
</asp:Content>
