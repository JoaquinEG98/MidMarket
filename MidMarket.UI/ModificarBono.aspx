<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarBono.aspx.cs" Inherits="MidMarket.UI.ModificarBono" MasterPageFile="~/Site.Master" Title="Modificar Bono" %>
<%@ Register Src="~/Controls/ValidarBonos.ascx" TagName="ValidarBonos" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-bonos">
        <h2 data-etiqueta="titulo_ModificarBono">Modificar Bono</h2>

        <uc:ValidarBonos ID="ValidarBonos" runat="server" />

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="submit-btn-bonos" OnClick="btnGuardar_Click" data-etiqueta="btn_Guardar" />
    </div>
</asp:Content>
