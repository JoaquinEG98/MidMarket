<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="MidMarket.UI.Carrito" MasterPageFile="~/Site.Master" Title="Carrito de Compras" %>

<%@ Import Namespace="MidMarket.Entities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="carrito-container">
        <h1 class="carrito-title" data-etiqueta="titulo_CarritoCompras">Tu Carrito de Compras</h1>

        <asp:Literal ID="ltlCarritoVacio" runat="server" Visible="false">
            <div class="mensaje-carrito-vacio">
                <img src="images/carritovacio.png" alt="Carrito vacío" class="img-carrito-vacio" />
                <p data-etiqueta="mensaje_NoTransacciones">Tu carrito está vacío.</p>
                <p data-etiqueta="mensaje_ComenzaInvertir">Comenzá a invertir ahora mismo en la sección <a href="Compra.aspx" class="link-compra-activos" data-etiqueta="link_CompraActivos">Compra de Activos</a></p>
            </div>
        </asp:Literal>

        <div id="divCarrito" runat="server" class="carrito-contenido">
            <table class="carrito-table">
                <thead>
                    <tr>
                        <th data-etiqueta="table_Producto">Producto</th>
                        <th data-etiqueta="table_Detalle">Detalle</th>
                        <th data-etiqueta="table_Cantidad">Cantidad</th>
                        <th data-etiqueta="table_Precio">Precio</th>
                        <th data-etiqueta="table_Accion">Acción</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCarrito" runat="server" OnItemDataBound="rptCarrito_ItemDataBound" OnItemCommand="rptCarrito_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Activo.Nombre") %></td>
                                <td id="tdDetalle" runat="server"></td>
                                <td>
                                    <asp:Button ID="btnDecrease" runat="server" CommandName="CambiarCantidad" CommandArgument='<%# Eval("Id") + ",-1" %>' Text="-" CssClass="carrito-button-confirm" data-etiqueta="btn_Decrease" />
                                    <asp:TextBox ID="cantidadInput" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="cantidad-input" ReadOnly="true" />
                                    <asp:Button ID="btnIncrease" runat="server" CommandName="CambiarCantidad" CommandArgument='<%# Eval("Id") + ",1" %>' Text="+" CssClass="carrito-button-confirm" data-etiqueta="btn_Increase" />
                                </td>
                                <td id="tdPrecioTasa" runat="server"></td>
                                <td>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarItem" CommandArgument='<%# Eval("Id") %>' CssClass="carrito-icon-remove" ToolTip="">
                                        <i class="fas fa-trash"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="carrito-total">
                <h2>
                    <span data-etiqueta="total_CarritoTexto">Total</span>: 
       
                    <span id="totalCarrito">$<%= ViewState["TotalCarrito"] != null ? ((decimal)ViewState["TotalCarrito"]).ToString("N2") : "0,00" %></span>
                </h2>
                <asp:Button ID="btnConfirmarCompra" runat="server" CssClass="carrito-button-confirm" OnClick="btnConfirmarCompra_Click" Text="Confirmar Compra" data-etiqueta="btn_ConfirmarCompra" />
            </div>
        </div>
    </div>
</asp:Content>
