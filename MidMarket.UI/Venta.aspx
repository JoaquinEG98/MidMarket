<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="MidMarket.UI.Venta" MasterPageFile="~/Site.Master" Title="Ventas" %>

<%@ Import Namespace="MidMarket.Entities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-ventas">
        <h2 class="ventas-title" data-etiqueta="titulo_Ventas">Ventas</h2>

        <asp:Literal ID="ltlVentasVacias" runat="server" Visible="false">
            <div class="mensaje-ventas-vacio">
                <img src="images/carritovacio.png" alt="Ventas vacías" class="img-ventas-vacio" />
                <p data-etiqueta="mensaje_VentasVacias">No tenés ventas registradas.</p>
                <p data-etiqueta="mensaje_ComenzaInvertir">Comenzá a invertir ahora mismo en la sección <a href="Compra.aspx" class="link-compra-activos" data-etiqueta="link_CompraActivosVentas">Compra de Activos</a></p>
            </div>
        </asp:Literal>

        <div id="divVentas" runat="server" class="ventas-contenido">
            <asp:Repeater ID="rptTransacciones" runat="server">
                <HeaderTemplate>
                    <table id="tablaVentas">
                        <thead>
                            <tr>
                                <th data-etiqueta="table_Activo">Activo</th>
                                <th data-etiqueta="table_Cantidad">Cantidad</th>
                                <th data-etiqueta="table_UltimoPrecio">Último Precio</th>
                                <th data-etiqueta="table_ValorNominal">Valor Nominal</th>
                                <th data-etiqueta="table_Rendimiento">Rendimiento</th>
                                <th data-etiqueta="table_CantidadVender">Cantidad a Vender</th>
                                <th data-etiqueta="table_Accion">Acción</th>
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

                                <td><%# (Eval("Activo") is Accion accion ? accion.Precio.ToString("N2") : "-") %></td>
                                <td><%# (Eval("Activo") is Bono bono ? bono.ValorNominal.ToString("N2") : "-") %></td>
                                <td>
                                    <%# 
                                        Eval("Activo") is Accion ? 
                                        (Convert.ToInt32(Eval("Cantidad")) * ((Accion)Eval("Activo")).Precio).ToString("N2") : 
                                        (Eval("Activo") is Bono ? (Convert.ToInt32(Eval("Cantidad")) * ((Bono)Eval("Activo")).ValorNominal).ToString("N2") : "-") 
                                    %>
                                </td>

                                <td>
                                    <div class="cantidad-container">
                                        <asp:Button ID="btnDecrease" runat="server" Text="-" CssClass="cantidad-boton" OnClientClick="return cambiarCantidad(this, -1);" data-etiqueta="btn_Decrease" />
                                        <asp:TextBox ID="cantidadInput" runat="server" Text="1" CssClass="cantidad-input" />
                                        <asp:Button ID="btnIncrease" runat="server" Text="+" CssClass="cantidad-boton" OnClientClick="return cambiarCantidad(this, 1);" data-etiqueta="btn_Increase" />
                                    </div>
                                </td>
                                <td>
                                    <asp:Button ID="btnVender"
                                        runat="server"
                                        CssClass="btn btn-vender"
                                        CommandArgument='<%# Eval("Activo.Id") %>'
                                        Text="Vender"
                                        OnClick="VenderActivo_Click"
                                        data-etiqueta="btn_Vender"
                                        data-max='<%# Eval("Cantidad") %>' />
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
    </div>

    <script type="text/javascript">
        function cambiarCantidad(button, cambio) {
            const cantidadInput = button.closest('td').querySelector('.cantidad-input');
            let cantidad = parseInt(cantidadInput.value) || 1;

            const maxCantidad = parseInt(button.closest('tr').querySelector('.btn-vender').getAttribute('data-max'));

            if (cambio === 1) {
                if (cantidad < maxCantidad) {
                    cantidad += cambio;
                }

                if (cantidad > maxCantidad) {
                    cantidad = maxCantidad;
                }
            } else {
                cantidad += cambio;
                if (cantidad < 1) {
                    cantidad = 1;
                }
            }

            cantidadInput.value = cantidad;

            return false;
        }
    </script>
</asp:Content>
