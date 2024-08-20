<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministracionBD.aspx.cs" Inherits="MidMarket.UI.AdministracionBD" MasterPageFile="~/Site.Master" Title="Administración de Base de Datos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="adminbd-container" class="adminbd-container">
        <h2>Administración de Base de Datos</h2>

        <div class="section">
            <h3>Generar Backup</h3>
            <div class="form-group">
                <label for="txtRutaBackup">Seleccione la ruta para guardar el Backup:</label>
                <div class="file-path-container">
                    <asp:TextBox ID="txtRutaBackup" runat="server" CssClass="file-path-input" Placeholder="C:\ruta\de\backup" />
                    <asp:Button ID="btnBrowseRutaBackup" runat="server" Text="..." CssClass="browse-btn" OnClick="btnBrowseRutaBackup_Click" />
                </div>
            </div>
            <asp:Button ID="btnGenerarBackup" runat="server" Text="Guardar Backup" CssClass="primary-btn" OnClick="btnGenerarBackup_Click" />
        </div>

        <div class="separator"></div>

        <div class="section">
            <h3>Restaurar Base de Datos</h3>
            <div class="form-group">
                <label for="fileUploadRestore">Seleccione el archivo de Backup:</label>
                <asp:FileUpload ID="fileUploadRestore" runat="server" CssClass="custom-file-upload" />
            </div>
            <asp:Button ID="btnRestaurarBD" runat="server" Text="Restaurar Base de Datos" CssClass="primary-btn" OnClick="btnRestaurarBD_Click" />
        </div>

        <div class="separator"></div>

        <div class="section">
            <h3>Dígitos Verificadores</h3>
            <div class="status-container">
                <span id="estadoDV" class="status-text correcto">Estado: Correcto</span>
            </div>
            <asp:Button ID="btnRecalcularDV" runat="server" Text="Recalcular Dígitos" CssClass="primary-btn" OnClick="btnRecalcularDigitos_Click" />
        </div>
    </div>
</asp:Content>