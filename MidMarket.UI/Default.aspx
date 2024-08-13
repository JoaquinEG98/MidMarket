<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MidMarket.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

       <div>
        <h2>Hola, <%: Cliente.RazonSocial %></h2>
        <asp:Button ID="btnLogout" runat="server" Text="Desloguear" OnClick="btnLogout_Click" />
    </div>

</asp:Content>
