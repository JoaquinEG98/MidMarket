<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MenuPrincipal.aspx.cs" Inherits="MidMarket.UI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="welcome-container">
        <h2><strong>Hola, <%: Cliente.RazonSocial %> (<%: Familia %>) 👋</strong></h2>
        <p>Acá podés ver el resumen de tu cuenta y las últimas novedades del mercado financiero.</p>
    </div>

    <div style="display: flex; gap: 20px; margin-bottom: 40px;">
        <div class="card">
            <h3>Balance Total</h3>
            <p>$<%: Cliente.Cuenta.Saldo.ToString("N2") %></p>
        </div>

        <div class="card">
            <h3>Inversiones Actuales</h3>
            <p>$<%: TotalInvertido.ToString("N2") %></p>
        </div>

        <div class="card">
            <h3>Última Transacción</h3>
            <p>$<%: UltimaTransaccion.ToString("N2") %></p>
        </div>
    </div>

    <div class="chart-container">
        <h3><strong>Compras de Activos</strong></h3>
        <canvas id="comprasChart" width="400" height="150"></canvas>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            var ctx = document.getElementById('comprasChart').getContext('2d');

            var labels = <%= LabelsJson %>;
            var accionesData = <%= AccionesDataJson %>;
            var bonosData = <%= BonosDataJson %>;

            var chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [
                        {
                            label: 'Acciones',
                            backgroundColor: '#7986CB',
                            borderColor: '#7986CB',
                            borderWidth: 1,
                            data: accionesData
                        },
                        {
                            label: 'Bonos',
                            backgroundColor: '#A5D6A7',
                            borderColor: '#A5D6A7',
                            borderWidth: 1,
                            data: bonosData
                        }
                    ]
                },
                options: {
                    responsive: true,
                    scales: {
                        x: {
                            stacked: true
                        },
                        y: {
                            stacked: true,
                            beginAtZero: true
                        }
                    },
                    title: {
                        display: true,
                        text: 'Compras de Activos por Fecha'
                    }
                }
            });
        });
    </script>
</asp:Content>
