<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaUsuario.aspx.cs" Inherits="MidMarket.UI.AltaUsuario" MasterPageFile="~/Site.Master" Title="Alta de Usuario" %>
<%@ Register Src="~/Controls/ValidarUsuario.ascx" TagName="ValidarUsuario" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-usuarios" class="container-usuarios">
        <form class="registro-usuario-form" method="post">
            <h2 data-etiqueta="titulo_AltaUsuario">Alta de Usuario</h2>

            <uc:ValidarUsuario ID="ValidarUsuarioControl" runat="server" />

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="submit-btn-usuarios" OnClick="btnRegistrar_Click" data-etiqueta="btn_RegistrarUsuario" />
        </form>
    </div>
</asp:Content>
