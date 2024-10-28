<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarUsuario.ascx.cs" Inherits="MidMarket.UI.ValidarUsuario" %>

<div class="form-group-usuarios">
    <label for="txtEmailUsuario">Email</label>
    <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="usuario-input form-group-usuarios" Placeholder="Email" TextMode="Email"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmailUsuario"
        ErrorMessage="El formato del email no es válido."
        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" Display="Dynamic" CssClass="error-message"></asp:RegularExpressionValidator>
</div>

<div class="form-group-usuarios">
    <label for="txtPasswordUsuario">Password</label>
    <asp:TextBox ID="txtPasswordUsuario" runat="server" CssClass="usuario-input form-group-usuarios" Placeholder="Password" TextMode="Password"></asp:TextBox>
    <asp:CustomValidator ID="cvPassword" runat="server" ControlToValidate="txtPasswordUsuario" 
        ErrorMessage="La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un carácter especial."
        OnServerValidate="ValidatePassword" Display="Dynamic" CssClass="error-message"></asp:CustomValidator>
</div>

<div class="form-group-usuarios">
    <label for="txtRazonSocialUsuario">Razón Social</label>
    <asp:TextBox ID="txtRazonSocialUsuario" runat="server" CssClass="usuario-input form-group-usuarios" Placeholder="Razón Social"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revRazonSocial" runat="server" ControlToValidate="txtRazonSocialUsuario"
        ErrorMessage="La razón social no debe superar los 30 caracteres."
        ValidationExpression="^.{0,30}$" Display="Dynamic" CssClass="error-message"></asp:RegularExpressionValidator>
</div>

<div class="form-group-usuarios">
    <label for="txtCUITUsuario">CUIT</label>
    <asp:TextBox ID="txtCUITUsuario" runat="server" CssClass="usuario-input form-group-usuarios" Placeholder="CUIT"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revCUIT" runat="server" ControlToValidate="txtCUITUsuario"
        ErrorMessage="El CUIT debe contener exactamente 11 dígitos."
        ValidationExpression="^\d{11}$" Display="Dynamic" CssClass="error-message"></asp:RegularExpressionValidator>
</div>
