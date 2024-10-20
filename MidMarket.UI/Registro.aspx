﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="MidMarket.UI.Registro" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>MidMarket - Registro</title>
    <link rel="stylesheet" href="Styles/registro_login_style.css">
    <script src="https://challenges.cloudflare.com/turnstile/v0/api.js" async defer></script>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server" method="post" class="registro-form">
            <h2>Registro</h2>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="email" id="txtEmail" name="correo" autocomplete="off" required runat="server">
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" id="txtPassword" name="password" required runat="server">
            </div>
            <div class="form-group">
                <label for="razonSocial">Razón Social</label>
                <input type="text" id="txtRazonSocial" name="razonSocial" autocomplete="off" required runat="server">
            </div>
            <div class="form-group">
                <label for="cuit">CUIT</label>
                <input type="text" id="txtCUIT" name="cuit" autocomplete="off" required runat="server">
            </div>
            <div class="form-group captcha">
                <div class="cf-turnstile" data-sitekey="0x4AAAAAAAhq4RxabKV10192" data-theme="light"></div>
            </div>
            <asp:Button type="submit" ID="btnRegistro" class="submit-btn" runat="server" Text="Registrarse" OnClick="btnRegistro_Click"></asp:Button>

            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" CssClass="error-label"></asp:Label>
        </form>
        <div class="extra-options">
            <p>¿Ya tienes cuenta? <a href="Login.aspx">Inicia sesión aquí</a></p>
        </div>
    </div>
</body>
</html>