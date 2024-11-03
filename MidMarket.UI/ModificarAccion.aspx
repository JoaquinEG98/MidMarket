<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarAccion.aspx.cs" Inherits="MidMarket.UI.ModificarAccion" MasterPageFile="~/Site.Master" Title="Modificar Acción" %>

<%@ Register Src="~/Controls/ValidarAcciones.ascx" TagName="ValidarAcciones" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-acciones" class="container-acciones">
        <h2 data-etiqueta="titulo_ModificarAccion">Modificar Acción</h2>

        <uc:ValidarAcciones ID="ValidarAcciones" runat="server" />

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="submit-btn-acciones" OnClick="btnGuardar_Click" data-etiqueta="btn_Guardar" />
    </div>
</asp:Content>
