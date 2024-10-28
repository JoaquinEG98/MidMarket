<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaBonos.aspx.cs" Inherits="MidMarket.UI.AltaBonos" MasterPageFile="~/Site.Master" Title="Alta de Bonos" %>
<%@ Register Src="~/Controls/ValidarBonos.ascx" TagName="ValidarBonos" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-bonos" class="container-bonos">
        <h2>Alta de Bono</h2>

        <uc:ValidarBonos ID="ValidarBonos" runat="server" />

        <asp:Button ID="btnCargarBono" runat="server" Text="Cargar" OnClick="btnCargarBono_Click" CssClass="submit-btn-bonos" />
    </div>
</asp:Content>
