<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="MidMarket.UI.Carrito" MasterPageFile="~/Site.Master" Title="Carrito de Compras" %>

<%@ Import Namespace="MidMarket.Entities" %>

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
                <asp:Repeater ID="rptCarrito" runat="server" OnItemDataBound="rptCarrito_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Activo.Nombre") %></td>
                            <td id="tdDetalle" runat="server"></td>
                            <!-- Se llenará desde el backend -->
                            <td>
                                <input type="number" value="<%# Eval("Cantidad") %>" min="1" class="cantidad-input">
                            </td>
                            <td id="tdPrecioTasa" runat="server"></td>
                            <!-- Se llenará desde el backend -->
                            <td><i class="fas fa-trash carrito-icon-remove"></i></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>


            </tbody>
        </table>

        <!-- Total y botones de acciones -->
        <div class="carrito-total">
            <h2>Total: <%= MiCarrito.Sum(item => item.Activo is Accion ? ((Accion)item.Activo).Precio : 0) %></h2>
            <button class="carrito-button-confirm">Confirmar Compra</button>
        </div>
    </div>
</asp:Content>
