<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarAccion.aspx.cs" Inherits="MidMarket.UI.ModificarAccion" MasterPageFile="~/Site.Master" Title="Modificar Acción" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-acciones">
        <form class="registro-form" method="post">
            <h2>Modificar Acción</h2>

            <div class="form-group-acciones">
                <label for="nombreAccion">Nombre de la Acción</label>
                <input type="text" id="nombreAccion" name="nombreAccion" value="<%= Accion.Nombre %>" class="form-control" required>
            </div>

            <div class="form-group-acciones">
                <label for="simboloAccion">Símbolo de la Acción</label>
                <input type="text" id="simboloAccion" name="simboloAccion" value="<%= Accion.Simbolo %>" class="form-control" required>
            </div>

            <div class="form-group-acciones">
                <label for="precioAccion">Precio de la Acción</label>
                <input type="text" id="precioAccion" name="precioAccion" value="<%= Accion.Precio.ToString("F2") %>" class="form-control" required>
            </div>

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="submit-btn-acciones" OnClick="btnGuardar_Click" />
        </form>
    </div>
</asp:Content>
