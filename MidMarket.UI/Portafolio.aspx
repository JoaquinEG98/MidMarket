<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Portafolio.aspx.cs" Inherits="MidMarket.UI.Portafolio" MasterPageFile="~/Site.Master" Title="Mi Portafolio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="~/Content/Portafolio.css" rel="stylesheet" />

    <div class="container mt-4 portafolio-container">
        <h2 class="portafolio-title">Portafolio</h2>

        <div class="d-flex justify-content-end mb-3">
            <button class="btn ingresar-dinero">Ingresar dinero</button>
        </div>

        <div class="row">
            <div class="col-md-8">
                <div class="activos-card">
                    <table class="table activos-table">
                        <thead class="table-primary">
                            <tr>
                                <th>Activo</th>
                                <th>Cantidad</th>
                                <th>Último Precio</th>
                                <th>Valor Nominal</th>
                                <th>Rendimiento</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Acciones</td>
                                <td>100</td>
                                <td>$1,500.00</td>
                                <td>-</td>
                                <td>$150.00</td>
                            </tr>
                            <tr>
                                <td>Bonos</td>
                                <td>50</td>
                                <td>-</td>
                                <td>$5,000.00</td>
                                <td>$250.00</td>
                            </tr>
                        </tbody>
                    </table>

                    <div class="disponible-operar">
                        <h3 class="disponible-titulo">Efectivo</h3>
                        <p class="disponible-item">
                            <span class="icono-moneda">$</span>
                            <span>PESOS</span>
                            <span class="disponible-texto">Disponible para operar</span>
                            <span class="disponible-monto">$ 197,00</span>
                        </p>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="custom-card resumen-card mb-3">
                    <div class="custom-card-header">Resumen</div>
                    <div class="custom-card-body">
                        <p><strong>Ganancia - Pérdida:</strong> <strong>$400.00</strong></p>
                        <p><strong>Activos Valorizados:</strong> <strong>$7,000.00</strong></p>
                    </div>
                </div>

                <div class="custom-card distribucion-card">
                    <div class="custom-card-header">Distribución de Activos</div>
                    <div class="custom-card-body">
                        <canvas id="chartDistribucionActivos"></canvas>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var ctx = document.getElementById('chartDistribucionActivos').getContext('2d');
            var chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Acciones', 'Bonos'],
                    datasets: [{
                        data: [100, 50],
                        backgroundColor: ['#FFCCB3', '#D4E6A5']
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            display: true,
                            position: 'bottom',
                            labels: {
                                color: '#333'
                            }
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>
