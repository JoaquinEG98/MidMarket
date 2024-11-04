<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarAcciones.ascx.cs" Inherits="MidMarket.UI.ValidarAcciones" %>

<div class="form-group-acciones">
    <label for="txtNombre" data-etiqueta="label_Nombre">Nombre</label>
    <asp:TextBox ID="txtNombre" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_NombreRequerido" />
    <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombre" ValidationExpression="^[a-zA-Z\s]{1,20}$" ErrorMessage="El nombre no debe exceder 20 caracteres." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_NombreLongitud" />
</div>

<div class="form-group-acciones">
    <label for="txtSimbolo" data-etiqueta="label_Simbolo">Símbolo</label>
    <asp:TextBox ID="txtSimbolo" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvSimbolo" runat="server" ControlToValidate="txtSimbolo" ErrorMessage="El símbolo es requerido." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_SimboloRequerido" />
    <asp:RegularExpressionValidator ID="revSimbolo" runat="server" ControlToValidate="txtSimbolo" ValidationExpression="^[a-zA-Z0-9\s]{1,20}$" ErrorMessage="El símbolo no debe exceder 20 caracteres." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_SimboloLongitud" />
</div>

<div class="form-group-acciones">
    <label for="txtPrecio" data-etiqueta="label_Precio">Precio</label>
    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="El precio es requerido." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_PrecioRequerido" />
    <asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtPrecio" ValidationExpression="^\d+([.,]\d{1,2})?$" ErrorMessage="Formato de precio incorrecto." Display="Dynamic" CssClass="text-danger" data-etiqueta="error_FormatoPrecio" />
</div>
