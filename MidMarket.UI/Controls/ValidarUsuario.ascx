﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarUsuario.ascx.cs" Inherits="MidMarket.UI.ValidarUsuario" %>

<div class="form-group-usuarios">
    <label for="txtEmailUsuario" data-etiqueta="label_Email">Email</label>
    <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="usuario-input form-group-usuarios" TextMode="Email"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmailUsuario"
        ErrorMessage="El formato del email no es válido." Display="Dynamic" CssClass="error-message" data-etiqueta="error_EmailFormato"></asp:RegularExpressionValidator>
</div>

<div class="form-group-usuarios">
    <label for="txtPasswordUsuario" data-etiqueta="label_Password">Password</label>
    <asp:TextBox ID="txtPasswordUsuario" runat="server" CssClass="usuario-input form-group-usuarios" TextMode="Password"></asp:TextBox>
    <asp:CustomValidator ID="cvPassword" runat="server" ControlToValidate="txtPasswordUsuario" 
        ErrorMessage="La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un carácter especial."
        OnServerValidate="ValidatePassword" Display="Dynamic" CssClass="error-message" data-etiqueta="error_PasswordRequisitos"></asp:CustomValidator>
</div>

<div class="form-group-usuarios">
    <label for="txtRazonSocialUsuario" data-etiqueta="label_RazonSocialUsuario">Razón Social</label>
    <asp:TextBox ID="txtRazonSocialUsuario" runat="server" CssClass="usuario-input form-group-usuarios"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revRazonSocial" runat="server" ControlToValidate="txtRazonSocialUsuario"
        ErrorMessage="La razón social no debe superar los 30 caracteres." Display="Dynamic" CssClass="error-message" data-etiqueta="error_RazonSocialLongitud"></asp:RegularExpressionValidator>
</div>

<div class="form-group-usuarios">
    <label for="txtCUITUsuario" data-etiqueta="label_CUITUsuario">CUIT</label>
    <asp:TextBox ID="txtCUITUsuario" runat="server" CssClass="usuario-input form-group-usuarios"></asp:TextBox>
    <asp:RegularExpressionValidator ID="revCUIT" runat="server" ControlToValidate="txtCUITUsuario"
        ErrorMessage="El CUIT debe contener exactamente 11 dígitos." Display="Dynamic" CssClass="error-message" data-etiqueta="error_CUITFormato"></asp:RegularExpressionValidator>
</div>
