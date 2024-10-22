<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="MidMarket.UI.Compra" MasterPageFile="~/Site.Master" Title="Compra de Acciones y Bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="compra-container">
        <!-- Sección de Acciones -->
        <h1 class="compra-title">Acciones</h1>
        <div class="compra-grid">
            <!-- Acción: Tesla -->
            <div class="compra-card compra-card-acciones">
                <h3>Tesla</h3>
                <div class="separator"></div>
                <p>Símbolo: TSLA</p>
                <p>Precio: $800.00</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
            
            <!-- Acción: Microsoft -->
            <div class="compra-card compra-card-acciones">
                <h3>Microsoft</h3>
                <div class="separator"></div>
                <p>Símbolo: MSFT</p>
                <p>Precio: $250.00</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
            
            <!-- Acción: Amazon -->
            <div class="compra-card compra-card-acciones">
                <h3>Amazon</h3>
                <div class="separator"></div>
                <p>Símbolo: AMZN</p>
                <p>Precio: $3200.00</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
            
            <!-- Acción: Google -->
            <div class="compra-card compra-card-acciones">
                <h3>Google</h3>
                <div class="separator"></div>
                <p>Símbolo: GOOGL</p>
                <p>Precio: $2800.00</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
            
            <!-- Acción: Facebook -->
            <div class="compra-card compra-card-acciones">
                <h3>Facebook</h3>
                <div class="separator"></div>
                <p>Símbolo: META</p>
                <p>Precio: $350.00</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
        </div>

        <!-- Sección de Bonos -->
        <h1 class="compra-title">Bonos</h1>
        <div class="compra-grid">
            <!-- Bono: Corporativo ABC -->
            <div class="compra-card compra-card-bonos">
                <h3>Bono Corporativo ABC</h3>
                <div class="separator"></div>
                <p>Valor Nominal: $1000</p>
                <p>Tasa de Interés: 5%</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>

            <!-- Bono: Estado XYZ -->
            <div class="compra-card compra-card-bonos">
                <h3>Bono del Estado XYZ</h3>
                <div class="separator"></div>
                <p>Valor Nominal: $5000</p>
                <p>Tasa de Interés: 3%</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>

            <!-- Bono: Corporativo DEF -->
            <div class="compra-card compra-card-bonos">
                <h3>Bono Corporativo DEF</h3>
                <div class="separator"></div>
                <p>Valor Nominal: $2000</p>
                <p>Tasa de Interés: 4%</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>

            <!-- Bono: Estado PQR -->
            <div class="compra-card compra-card-bonos">
                <h3>Bono del Estado PQR</h3>
                <div class="separator"></div>
                <p>Valor Nominal: $3000</p>
                <p>Tasa de Interés: 2%</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>

            <!-- Bono: Corporativo XYZ -->
            <div class="compra-card compra-card-bonos">
                <h3>Bono Corporativo XYZ</h3>
                <div class="separator"></div>
                <p>Valor Nominal: $7000</p>
                <p>Tasa de Interés: 6%</p>
                <button class="compra-button">Agregar al Carrito</button>
            </div>
        </div>
    </div>
</asp:Content>
