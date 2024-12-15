<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarUsuario.aspx.cs" Inherits="MidMarket.UI.ModificarUsuario" MasterPageFile="~/Site.Master" Title="Modificar Usuario" %>
<%@ Register Src="~/Controls/ValidarUsuario.ascx" TagName="ValidarUsuario" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-usuarios">
        <form class="registro-form" method="post">
            <h2 data-etiqueta="titulo_ModificarUsuario">Modificar Usuario</h2>

            <uc:ValidarUsuario ID="ValidarUsuarioControl" runat="server" />

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="submit-btn-usuarios" OnClick="btnGuardar_Click" data-etiqueta="btn_GuardarUsuario" />
        </form>
    </div>
</asp:Content>