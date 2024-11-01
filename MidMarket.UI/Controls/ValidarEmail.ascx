﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidarEmail.ascx.cs" Inherits="MidMarket.UI.ValidarEmail" %>

<asp:TextBox ID="txtEmail" runat="server" CssClass="form-group input" placeholder="Introduce tu correo electrónico" />

<asp:RequiredFieldValidator 
    ID="rfvEmail" 
    runat="server" 
    ControlToValidate="txtEmail" 
    ErrorMessage="El email es requerido." 
    Display="Dynamic" 
    ForeColor="Red" 
    CssClass="error-message" />

<asp:RegularExpressionValidator 
    ID="regexEmailValidator" 
    runat="server" 
    ControlToValidate="txtEmail"
    ErrorMessage="Formato de correo inválido" 
    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" 
    ForeColor="Red" 
    Display="Dynamic" 
    CssClass="error-message" />