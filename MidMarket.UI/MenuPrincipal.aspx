<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="MidMarket.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="welcome-container">
        <h2><strong>Hola, <%: Cliente.RazonSocial %> (<%: Familia %>) 👋</strong></h2>
        <p>Acá podés ver el resumen de tu cuenta y las últimas novedades del mercado financiero.</p>
    </div>

    <div style="display: flex; gap: 20px; margin-bottom: 40px;">
        <div class="card">
            <h3>Balance Total</h3>
            <p>$<%: Cliente.Cuenta.Saldo %></p>
        </div>

        <div class="card">
            <h3>Inversiones Actuales</h3>
            <p>$0</p>
        </div>

        <div class="card">
            <h3>Ganancias del Mes</h3>
            <p>$0</p>
        </div>
    </div>

    <div class="chart-container">
        <h3><strong>Rendimiento de Inversiones</strong></h3>
        <canvas id="rendimientoChart" width="400" height="150"></canvas>
    </div>

    <div>
        <h3 style="color: #7e57c2;"><strong>Últimas Noticias del Mercado</strong></h3>
        <ul class="news-list">
            <li class="news-item">
                <strong>Noticia 1:</strong> Las acciones de tecnología suben un 5% en el último trimestre.
            </li>
            <li class="news-item">
                <strong>Noticia 2:</strong> La Fed mantiene las tasas de interés sin cambios.
            </li>
        </ul>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            var ctx = document.getElementById('rendimientoChart').getContext('2d');
            var chart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul'],
                    datasets: [{
                        label: 'Rendimiento de Inversiones',
                        backgroundColor: 'rgba(94, 45, 180, 0.2)',
                        borderColor: '#5e2db4',
                        data: [1200, 1400, 1300, 1500, 1700, 1600, 1800]
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