<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReestablecerPassword.aspx.cs" Inherits="MidMarket.UI.ReestablecerPassword" %>
<%@ Register Src="~/ValidarEmail.ascx" TagPrefix="uc" TagName="ValidarEmail" %>


<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>MidMarket - Reestablecer Contraseña</title>
    <link rel="stylesheet" href="Styles/registro_login_style.css">
</head>
<body>
    <div class="container">
        <form id="formSolicitud" runat="server" class="registro-form" visible="false">
            <h2>Restaurar Contraseña</h2>
            <div class="form-group">
                <label for="email">Email</label>
                <uc:ValidarEmail ID="ValidarEmailControl" runat="server" />
            </div>
            <asp:Button ID="btnReestablecer" runat="server" Text="Solicitar Enlace de Restauración" OnClick="btnReestablecer_Click" CssClass="submit-btn" />
        </form>
    </div>
</body>
</html>