<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="MidMarket.UI.Venta" MasterPageFile="~/Site.Master" Title="Ventas" %>

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
                <tr>
                    <td>Ternium Argentina</td>
                    <td>3</td>
                    <td>$ 8.150,00</td>
                    <td>-</td>
                    <td>$ 24.450,00</td>
                    <td><input type="number" class="cantidad-vender" value="1" min="1" max="3"></td>
                    <td><button class="btn btn-vender">Vender</button></td>
                </tr>
                <tr>
                    <td>Cresud</td>
                    <td>5</td>
                    <td>$ 3.950,00</td>
                    <td>-</td>
                    <td>$ 19.750,00</td>
                    <td><input type="number" class="cantidad-vender" value="1" min="1" max="5"></td>
                    <td><button class="btn btn-vender">Vender</button></td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
