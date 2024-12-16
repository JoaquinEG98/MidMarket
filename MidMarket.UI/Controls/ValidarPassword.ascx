<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarPassword.ascx.cs" Inherits="MidMarket.UI.ValidarPassword" %>

<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="30" CssClass="password-input" Placeholder=""></asp:TextBox>
<asp:CustomValidator ID="cvPassword" runat="server" ControlToValidate="txtPassword" 
    ErrorMessage="La contraseña debe tener al menos 8 caracteres, una mayúscula y un carácter especial." 
    OnServerValidate="ValidatePassword" Display="Dynamic" CssClass="error-message" data-etiqueta="error_PasswordRequisitos"></asp:CustomValidator>