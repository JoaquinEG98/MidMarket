﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaAcciones.aspx.cs" Inherits="MidMarket.UI.AltaAcciones" MasterPageFile="~/Site.Master" Title="Alta de Acciones" %>
<%@ Register Src="~/ValidarAcciones.ascx" TagName="ValidarAcciones" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-acciones" class="container-acciones">
        <form class="registro-acciones-form" method="post">
            <h2>Alta de Acción</h2>

            <uc:ValidarAcciones ID="ValidarAcciones" runat="server" />

            <asp:Button ID="btnCargar" runat="server" Text="Cargar" OnClick="btnCargar_Click" CssClass="submit-btn-acciones" />
        </form>
    </div>
</asp:Content>

