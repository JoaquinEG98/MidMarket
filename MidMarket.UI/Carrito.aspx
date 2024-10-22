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
                    <th>Precio</th>
                    <th>Acción</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptCarrito" runat="server" OnItemDataBound="rptCarrito_ItemDataBound" OnItemCommand="rptCarrito_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Activo.Nombre") %></td>
                            <td id="tdDetalle" runat="server"></td>
                            <!-- Se llenará desde el backend -->
                            <td>
                                <!-- Botones para aumentar/disminuir la cantidad -->
                                <asp:Button ID="btnDecrease" runat="server" CommandName="ChangeQuantity" CommandArgument='<%# Eval("Id") + ",-1" %>' Text="-" CssClass="carrito-button-confirm cantidad-boton" />
                                <asp:TextBox ID="cantidadInput" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="cantidad-input" ReadOnly="true" />
                                <asp:Button ID="btnIncrease" runat="server" CommandName="ChangeQuantity" CommandArgument='<%# Eval("Id") + ",1" %>' Text="+" CssClass="carrito-button-confirm cantidad-boton" />
                            </td>
                            <td id="tdPrecioTasa" runat="server"></td>
                            <!-- Ícono de la papelera para eliminar el producto -->
                            <td>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("Id") %>' CssClass="carrito-icon-remove" ToolTip="Eliminar del carrito">
                    <i class="fas fa-trash"></i>
                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <!-- Total y botones de acciones -->
        <div class="carrito-total">
            <h2>Total: <%= ViewState["TotalCarrito"] != null ? ((decimal)ViewState["TotalCarrito"]).ToString("F2") : "0.00" %></h2>
            <button class="carrito-button-confirm">Confirmar Compra</button>
        </div>
    </div>
</asp:Content>
