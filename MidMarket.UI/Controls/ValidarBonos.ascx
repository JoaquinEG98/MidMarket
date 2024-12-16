<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarBonos.ascx.cs" Inherits="MidMarket.UI.ValidarBonos" %>

<div class="form-group-bonos">
    <label for="txtNombreBono" data-etiqueta="label_NombreBono">Nombre</label>
    <asp:TextBox ID="txtNombreBono" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombreBono" runat="server" ControlToValidate="txtNombreBono" ErrorMessage="El nombre es requerido." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_NombreBonoRequerido" />
    <asp:RegularExpressionValidator ID="revNombreBono" runat="server" ControlToValidate="txtNombreBono" ValidationExpression="^[a-zA-Z0-9\s]{1,20}$" ErrorMessage="El nombre no debe exceder 20 caracteres." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_NombreBonoLongitud" />
</div>

<div class="form-group-bonos">
    <label for="txtValorNominal" data-etiqueta="label_ValorNominal">Valor Nominal</label>
    <asp:TextBox ID="txtValorNominal" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvValorNominal" runat="server" ControlToValidate="txtValorNominal" ErrorMessage="El valor nominal es requerido." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_ValorNominalRequerido" />
    <asp:RegularExpressionValidator ID="revValorNominal" runat="server" ControlToValidate="txtValorNominal" ValidationExpression="^\d+([.,]\d{1,2})?$" ErrorMessage="Formato de valor nominal incorrecto." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_FormatoValorNominal" />
</div>

<div class="form-group-bonos">
    <label for="txtTasaInteres" data-etiqueta="label_TasaInteres">Tasa de Interés (%)</label>
    <asp:TextBox ID="txtTasaInteres" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvTasaInteres" runat="server" ControlToValidate="txtTasaInteres" ErrorMessage="La tasa de interés es requerida." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_TasaInteresRequerido" />
    <asp:RegularExpressionValidator ID="revTasaInteres" runat="server" ControlToValidate="txtTasaInteres" ValidationExpression="^\d+([.,]\d{1,2})?$" ErrorMessage="Formato de tasa de interés incorrecto." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_FormatoTasaInteres" />
</div>
