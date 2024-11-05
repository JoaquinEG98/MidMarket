<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReestablecerPassword.aspx.cs" Inherits="MidMarket.UI.ReestablecerPassword" %>
<%@ Register Src="~/Controls/ValidarEmail.ascx" TagPrefix="uc" TagName="ValidarEmail" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title data-etiqueta="titulo_ReestablecerPassword">MidMarket - Reestablecer Contraseña</title>
    <link rel="stylesheet" href="Styles/registro_login_style.css">
</head>
<body>
    <div class="container">
        <form id="formSolicitud" runat="server" class="registro-form" visible="false">
            <h2 data-etiqueta="titulo_ReestablecerPassword">Restaurar Contraseña</h2>
            <div class="form-group">
                <label for="email" data-etiqueta="label_Email">Email</label>
                <uc:ValidarEmail ID="ValidarEmailControl" runat="server" />
            </div>
            <asp:Button ID="btnReestablecer" runat="server" Text="Reestablecer" OnClick="btnReestablecer_Click" CssClass="submit-btn" data-etiqueta="btn_Reestablecer" />
            <br />
            <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="message-label"></asp:Label>
        </form>
    </div>

    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll("[data-etiqueta]").forEach(function (element) {
                const etiqueta = element.getAttribute("data-etiqueta");

                if (traducciones && traducciones[etiqueta]) {
                    let textoTraducido = traducciones[etiqueta];

                    if (element.hasAttribute("data-simbolo")) {
                        const simbolo = element.getAttribute("data-simbolo");
                        textoTraducido = textoTraducido.replace("{Simbolo}", simbolo);
                    }

                    if (element.hasAttribute("data-precio")) {
                        const precio = element.getAttribute("data-precio");
                        textoTraducido = textoTraducido.replace("{Precio}", precio);
                    }

                    if (element.hasAttribute("data-valornominal")) {
                        const valorNominal = element.getAttribute("data-valornominal");
                        textoTraducido = textoTraducido.replace("{ValorNominal}", valorNominal);
                    }

                    if (element.hasAttribute("data-tasainteres")) {
                        const tasaInteres = element.getAttribute("data-tasainteres");
                        textoTraducido = textoTraducido.replace("{TasaInteres}", tasaInteres);
                    }

                    if (element.tagName === "A") {
                        element.textContent = textoTraducido;
                    } else if (element.tagName === "INPUT" && element.type === "submit") {
                        element.value = textoTraducido;
                    } else {
                        if (element.querySelector("a")) {
                            element.firstChild.textContent = textoTraducido;
                        } else {
                            element.textContent = textoTraducido;
                        }
                    }
                }
            });
        });
    </script>
</body>
</html>
