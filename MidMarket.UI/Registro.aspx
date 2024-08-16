<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="MidMarket.UI.Registro" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Registro</title>
    <link rel="stylesheet" href="Styles/registro_login_style.css">
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server" method="post" class="registro-form">
            <h2>Registro</h2>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="email" id="txtEmail" name="email" required runat="server">
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" id="txtPassword" name="password" required runat="server">
            </div>
            <div class="form-group">
                <label for="razonSocial">Razón Social</label>
                <input type="text" id="txtRazonSocial" name="razonSocial" required runat="server">
            </div>
            <div class="form-group">
                <label for="cuit">CUIT</label>
                <input type="text" id="txtCUIT" name="cuit" required runat="server">
            </div>
            <div class="form-group captcha">
                <div class="g-recaptcha" data-sitekey="YOUR_SITE_KEY"></div>
            </div>
            <asp:Button type="submit" id="btnRegistro" class="submit-btn" runat="server" Text="Registrarse" OnClick="btnRegistro_Click"></asp:Button>
        </form>
        <div class="extra-options">
            <p>¿Ya tienes cuenta? <a href="Login.aspx">Inicia sesión aquí</a></p>
        </div>
    </div>
</body>
</html>