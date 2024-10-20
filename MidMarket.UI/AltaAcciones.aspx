<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaAcciones.aspx.cs" Inherits="MidMarket.UI.AltaAcciones" MasterPageFile="~/Site.Master" Title="Alta de Acciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-acciones" class="container-acciones">
        <form class="registro-acciones-form" method="post">
            <h2>Alta de Acción</h2>

            <div class="form-group-acciones">
                <label for="nombreAccion">Nombre</label>
                <input type="text" id="nombreAccion" name="nombreAccion" required runat="server" />
            </div>

            <div class="form-group-acciones">
                <label for="simboloAccion">Símbolo</label>
                <input type="text" id="simboloAccion" name="simboloAccion" required runat="server" />
            </div>

            <div class="form-group-acciones">
                <label for="precioAccion">Precio</label>
                <input type="text" id="precioAccion" name="precioAccion" required runat="server" />
            </div>

            <asp:Button ID="btnCargar" runat="server" Text="Cargar" OnClick="btnCargar_Click" CssClass="submit-btn-acciones" />
        </form>
    </div>
</asp:Content>
