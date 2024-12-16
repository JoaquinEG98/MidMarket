<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MidMarket.UI.Login" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title data-etiqueta="titulo_Login">MidMarket - Login</title>
    <link rel="stylesheet" href="Styles/registro_login_style.css">
</head>
<body>
    <div class="container">
        <form id="form1" runat="server" method="post" class="registro-form">
            <h2 data-etiqueta="titulo_IniciarSesion">Iniciar sesión</h2>
            <div class="form-group">
                <label for="txtEmail" data-etiqueta="lbl_Email">Email</label>
                <input type="email" maxlength="30" id="txtEmail" name="correo" autocomplete="off" required runat="server">
            </div>
            <div class="form-group">
                <label for="txtPassword" data-etiqueta="lbl_Password">Password</label>
                <input type="password" maxlength="30" id="txtPassword" name="password" required runat="server">
            </div>
            <asp:Button type="submit" id="btnLogin" class="submit-btn" runat="server" Text="Login" OnClick="btnLogin_Click" data-etiqueta="btn_Login"></asp:Button>

            <br />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" CssClass="error-label"></asp:Label>
        </form>
        <div class="extra-options">
            <p><a href="reestablecerpassword.aspx" data-etiqueta="link_OlvidoPassword">¿Olvidaste tu contraseña?</a></p>
        </div>
    </div>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll("[data-etiqueta]").forEach(function (element) {
                const etiqueta = element.getAttribute("data-etiqueta");
                if (traducciones && traducciones[etiqueta]) {
                    element.textContent = traducciones[etiqueta];
                }
            });
        });
    </script>
</body>
</html>
