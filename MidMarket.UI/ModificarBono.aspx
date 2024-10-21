<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarBono.aspx.cs" Inherits="MidMarket.UI.ModificarBono" MasterPageFile="~/Site.Master" Title="Modificar Bono" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-bonos">
        <form class="registro-form" method="post">
            <h2>Modificar Bono</h2>

            <div class="form-group-bonos">
                <label for="nombreBono">Nombre del Bono</label>
                <input type="text" id="nombreBono" name="nombreBono" value="<%= Bono.Nombre %>" class="form-control" required>
            </div>

            <div class="form-group-bonos">
                <label for="valorNominal">Valor Nominal</label>
                <input type="text" id="valorNominal" name="valorNominal" value="<%= Bono.ValorNominal.ToString("F2") %>" class="form-control" required>
            </div>

            <div class="form-group-bonos">
                <label for="tasaInteres">Tasa de Interés (%)</label>
                <input type="text" id="tasaInteres" name="tasaInteres" value="<%= Bono.TasaInteres.ToString("F2") %>" class="form-control" required>
            </div>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="submit-btn-bonos" OnClick="btnGuardar_Click" />
        </form>
    </div>
</asp:Content>
