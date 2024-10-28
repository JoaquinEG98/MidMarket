<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarPassword.aspx.cs" Inherits="MidMarket.UI.CambiarPassword" MasterPageFile="~/Site.Master" Title="Cambio de Password" %>
<%@ Register Src="~/Controls/ValidarPassword.ascx" TagName="ValidarPassword" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-password">
        <form class="registro-password-form" method="post">
            <h2>Cambio de contraseña</h2>

            <div class="form-group-password">
                <label for="passwordActual">Contraseña actual</label>
                <input type="password" id="passwordActual" name="passwordActual" required runat="server" />
            </div>

            <div class="form-group-password">
                <label for="nuevoPassword">Nueva contraseña</label>
                <uc:ValidarPassword ID="ValidarPasswordControl" runat="server" />
            </div>

            <div class="form-group-password">
                <label for="confirmarPassword">Confirmar nueva contraseña</label>
                <input type="password" id="confirmarPassword" name="confirmarPassword" required runat="server" />
            </div>

            <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" OnClick="btnCambiar_Click" CssClass="submit-btn-password" />
        </form>
    </div>
</asp:Content>
