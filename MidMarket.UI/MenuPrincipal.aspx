<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="MidMarket.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Bienvenida -->
    <div style="padding: 20px;">
        <h2 style="color: #5e2db4;">Hola, <%: Cliente.RazonSocial %> (<%: Familia %>) 👋</h2>
        <p style="font-size: 18px;">Aquí puedes ver el resumen de tu cuenta y las últimas novedades del mercado financiero.</p>
    </div>

    <!-- Resumen de la cuenta -->
    <div style="display: flex; gap: 20px; margin-bottom: 40px;">
        <!-- Sección de balance -->
        <div style="flex: 1; padding: 20px; background-color: #f5f5f5; border-radius: 8px;">
            <h3 style="color: #5e2db4;">Balance Total</h3>
            <p style="font-size: 24px; color: #333;">$<%: Cliente.Cuenta.Saldo %></p>
        </div>

        <!-- Inversiones actuales -->
        <div style="flex: 1; padding: 20px; background-color: #f5f5f5; border-radius: 8px;">
            <h3 style="color: #5e2db4;">Inversiones Actuales</h3>
            <p style="font-size: 24px; color: #333;">1111</p>
        </div>

        <!-- Ganancias del mes -->
        <div style="flex: 1; padding: 20px; background-color: #f5f5f5; border-radius: 8px;">
            <h3 style="color: #5e2db4;">Ganancias del Mes</h3>
            <p style="font-size: 24px; color: #333;">2222</p>
        </div>
    </div>

    <!-- Gráfico de rendimiento -->
    <div style="margin-bottom: 40px;">
        <h3 style="color: #5e2db4;">Rendimiento de Inversiones</h3>
        <canvas id="rendimientoChart" width="400" height="150"></canvas>
    </div>

    <!-- Noticias del mercado -->
    <div>
        <h3 style="color: #5e2db4;">Últimas Noticias del Mercado</h3>
        <ul id="marketNews" style="list-style: none; padding: 0;">
            <li style="padding: 10px; background-color: #f5f5f5; margin-bottom: 10px; border-radius: 8px;">
                <strong>Noticia 1:</strong> Las acciones de tecnología suben un 5% en el último trimestre.
            </li>
            <li style="padding: 10px; background-color: #f5f5f5; margin-bottom: 10px; border-radius: 8px;">
                <strong>Noticia 2:</strong> La Fed mantiene las tasas de interés sin cambios.
            </li>
            <!-- Más noticias pueden ser cargadas dinámicamente -->
        </ul>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script>
    document.addEventListener('DOMContentLoaded', function () {
        // Configuración del gráfico
        var ctx = document.getElementById('rendimientoChart').getContext('2d');
        var chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul'],
                datasets: [{
                    label: 'Rendimiento de Inversiones',
                    backgroundColor: 'rgba(94, 45, 180, 0.2)',
                    borderColor: '#5e2db4',
                    data: [1200, 1400, 1300, 1500, 1700, 1600, 1800] // Datos de ejemplo
                }]
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Rendimiento en los últimos meses'
                }
            }
        });
    });
</script>


</asp:Content>

