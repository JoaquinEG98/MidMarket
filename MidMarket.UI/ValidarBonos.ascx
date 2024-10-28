<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarBonos.ascx.cs" Inherits="MidMarket.UI.ValidarBonos" %>

<div class="form-group-bonos">
    <label for="txtNombreBono">Nombre</label>
    <asp:TextBox ID="txtNombreBono" runat="server" MaxLength="30" CssClass="form-control" placeholder="Nombre del Bono"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombreBono" runat="server" ControlToValidate="txtNombreBono" ErrorMessage="El nombre es requerido." Display="Dynamic" CssClass="text-danger" />
    <asp:RegularExpressionValidator ID="revNombreBono" runat="server" ControlToValidate="txtNombreBono" ValidationExpression="^[a-zA-Z0-9\s]{1,30}$" ErrorMessage="El nombre no debe exceder 30 caracteres." Display="Dynamic" CssClass="text-danger" />
</div>

<div class="form-group-bonos">
    <label for="txtValorNominal">Valor Nominal</label>
    <asp:TextBox ID="txtValorNominal" runat="server" CssClass="form-control" placeholder="Valor Nominal"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvValorNominal" runat="server" ControlToValidate="txtValorNominal" ErrorMessage="El valor nominal es requerido." Display="Dynamic" CssClass="text-danger" />
    <asp:RegularExpressionValidator ID="revValorNominal" runat="server" ControlToValidate="txtValorNominal" ValidationExpression="^\d+([.,]\d{1,2})?$" ErrorMessage="Formato de valor nominal incorrecto." Display="Dynamic" CssClass="text-danger" />
</div>

<div class="form-group-bonos">
    <label for="txtTasaInteres">Tasa de Interés (%)</label>
    <asp:TextBox ID="txtTasaInteres" runat="server" CssClass="form-control" placeholder="Tasa de Interés"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvTasaInteres" runat="server" ControlToValidate="txtTasaInteres" ErrorMessage="La tasa de interés es requerida." Display="Dynamic" CssClass="text-danger" />
    <asp:RegularExpressionValidator ID="revTasaInteres" runat="server" ControlToValidate="txtTasaInteres" ValidationExpression="^\d+([.,]\d{1,2})?$" ErrorMessage="Formato de tasa de interés incorrecto." Display="Dynamic" CssClass="text-danger" />
</div>
