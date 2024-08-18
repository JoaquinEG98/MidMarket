<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MidMarket.UI.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>MidMarket - Login</title>
    <link rel="stylesheet" href="Styles/registro_login_style.css">
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server" method="post" class="registro-form">
            <h2>Iniciar sesión</h2>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="email" id="txtEmail" name="email" required runat="server">
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" id="txtPassword" name="password" required runat="server">
            </div>
            <asp:Button type="submit" id="btnLogin" class="submit-btn" runat="server" Text="Login" OnClick="btnLogin_Click"></asp:Button>
        </form>
        <div class="extra-options">
            <p>¿No tienes cuenta? <a href="registro.aspx">Regístrate aquí</a></p>
            <p><a href="RecuperarPassword.aspx">¿Olvidaste tu contraseña?</a></p>
        </div>
    </div>
</body>
</html>