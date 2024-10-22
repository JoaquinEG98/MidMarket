<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="MidMarket.UI.Carrito" MasterPageFile="~/Site.Master" Title="Carrito de Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="carrito-container">
        <h1 class="carrito-title">Tu Carrito de Compras</h1>

        <!-- Tabla con los productos del carrito -->
        <table class="carrito-table">
            <thead>
                <tr>
                    <th>Producto</th>
                    <th>Detalle</th>
                    <th>Cantidad</th>
                    <th>Precio/Tasa</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
                <!-- Ejemplo de un producto en el carrito -->
                <tr>
                    <td>Acción Tesla</td>
                    <td>Símbolo: TSLA</td>
                    <td>
                        <input type="number" value="1" min="1" class="cantidad-input">
                    </td>
                    <td>$800.00</td>
                    <td><i class="fas fa-trash carrito-icon-remove"></i></td>
                </tr>
                <tr>
                    <td>Bono Corporativo ABC</td>
                    <td>Valor Nominal: $1000</td>
                    <td>
                        <input type="number" value="1" min="1" class="cantidad-input">
                    </td>
                    <td>Tasa: 5%</td>
                    <td><i class="fas fa-trash carrito-icon-remove"></i></td>
                </tr>
            </tbody>
        </table>

        <!-- Total y botones de acciones -->
        <div class="carrito-total">
            <h2>Total: $3800.00</h2>
            <button class="carrito-button-confirm">Confirmar Compra</button>
        </div>
    </div>
</asp:Content>
