<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarAcciones.ascx.cs" Inherits="MidMarket.UI.ValidarAcciones" %>

<div class="form-group-acciones">
    <label for="txtNombre">Nombre</label>
    <asp:TextBox ID="txtNombre" runat="server" MaxLength="20" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido." Display="Dynamic" CssClass="text-danger" />
    <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombre" ValidationExpression="^[a-zA-Z\s]{1,20}$" ErrorMessage="El nombre no debe exceder 20 caracteres." Display="Dynamic" CssClass="text-danger" />
</div>

<div class="form-group-acciones">
    <label for="txtSimbolo">Símbolo</label>
    <asp:TextBox ID="txtSimbolo" runat="server" MaxLength="20" CssClass="form-control" placeholder="Símbolo"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvSimbolo" runat="server" ControlToValidate="txtSimbolo" ErrorMessage="El símbolo es requerido." Display="Dynamic" CssClass="text-danger" />
    <asp:RegularExpressionValidator ID="revSimbolo" runat="server" ControlToValidate="txtSimbolo" ValidationExpression="^[a-zA-Z0-9\s]{1,20}$" ErrorMessage="El símbolo no debe exceder 20 caracteres." Display="Dynamic" CssClass="text-danger" />
</div>

<div class="form-group-acciones">
    <label for="txtPrecio">Precio</label>
    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="Precio"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="El precio es requerido." Display="Dynamic" CssClass="text-danger" />
    <asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtPrecio" ValidationExpression="^\d+([.,]\d{1,2})?$" ErrorMessage="Formato de precio incorrecto." Display="Dynamic" CssClass="text-danger" />
</div>
