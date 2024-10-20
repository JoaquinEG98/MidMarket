<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaBonos.aspx.cs" Inherits="MidMarket.UI.AltaBonos" MasterPageFile="~/Site.Master" Title="Alta de Bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-bonos" class="container-bonos">
        <form class="registro-bonos-form" method="post">
            <h2>Alta de Bono</h2>

            <div class="form-group-bonos">
                <label for="nombreBono">Nombre</label>
                <input type="text" id="nombreBono" name="nombreBono" required runat="server" />
            </div>

            <div class="form-group-bonos">
                <label for="valorNominal">Valor Nominal</label>
                <input type="text" id="valorNominal" name="valorNominal" required runat="server" />
            </div>

            <div class="form-group-bonos">
                <label for="tasaInteres">Tasa de Interés (%)</label>
                <input type="text" id="tasaInteres" name="tasaInteres" required runat="server" />
            </div>

            <asp:Button ID="btnCargarBono" runat="server" Text="Cargar" OnClick="btnCargarBono_Click" CssClass="submit-btn-bonos" />
        </form>
    </div>
</asp:Content>