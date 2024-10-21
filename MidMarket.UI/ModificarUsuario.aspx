<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarUsuario.aspx.cs" Inherits="MidMarket.UI.ModificarUsuario" MasterPageFile="~/Site.Master" Title="Modificar Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-usuarios">
        <form class="registro-form" method="post">
            <h2>Modificar Usuario</h2>

            <div class="form-group-usuarios">
                <label for="emailUsuario">Email</label>
                <input type="email" id="emailUsuario" name="emailUsuario" value="<%= Usuario.Email %>" class="form-control" required>
            </div>

            <div class="form-group-usuarios">
                <label for="razonSocialUsuario">Razón Social</label>
                <input type="text" id="razonSocialUsuario" name="razonSocialUsuario" value="<%= Usuario.RazonSocial %>" class="form-control" required>
            </div>

            <div class="form-group-usuarios">
                <label for="cuitUsuario">CUIT</label>
                <input type="text" id="cuitUsuario" name="cuitUsuario" value="<%= Usuario.CUIT %>" class="form-control" required>
            </div>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="submit-btn-usuarios" OnClick="btnGuardar_Click" />
        </form>
    </div>
</asp:Content>
