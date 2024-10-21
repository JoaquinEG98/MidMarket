<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaUsuario.aspx.cs" Inherits="MidMarket.UI.AltaUsuario" MasterPageFile="~/Site.Master" Title="Alta de Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-usuarios" class="container-usuarios">
        <form class="registro-usuario-form" method="post">
            <h2>Alta de Usuario</h2>

            <div class="form-group-usuarios">
                <label for="emailUsuario">Email</label>
                <input type="email" id="emailUsuario" name="emailUsuario" required runat="server" />
            </div>

            <div class="form-group-usuarios">
                <label for="passwordUsuario">Password</label>
                <input type="password" id="passwordUsuario" name="passwordUsuario" required runat="server" />
            </div>

            <div class="form-group-usuarios">
                <label for="razonSocialUsuario">Razón Social</label>
                <input type="text" id="razonSocialUsuario" name="razonSocialUsuario" required runat="server" />
            </div>

            <div class="form-group-usuarios">
                <label for="cuitUsuario">CUIT</label>
                <input type="text" id="cuitUsuario" name="cuitUsuario" required runat="server" />
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" CssClass="submit-btn-usuarios" />
        </form>
    </div>
</asp:Content>
