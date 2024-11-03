<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Portafolio.aspx.cs" Inherits="MidMarket.UI.Portafolio" MasterPageFile="~/Site.Master" Title="Mi Portafolio" %>

<%@ Import Namespace="MidMarket.Entities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4 portafolio-container">
        <h2 class="portafolio-title" data-etiqueta="text_Resumen">Portafolio</h2>

        <div class="d-flex justify-content-end mb-3">
            <asp:HyperLink ID="btnIngresarDinero" runat="server" NavigateUrl="~/CargarSaldo.aspx" CssClass="btn ingresar-dinero" data-etiqueta="btn_IngresarDinero">
                Ingresar dinero
            </asp:HyperLink>
        </div>

        <div class="row">
            <div class="col-md-8">
                <div class="activos-card">
                    <table class="table activos-table">
                        <thead class="table-primary">
                            <tr>
                                <th data-etiqueta="table_Activo">Activo</th>
                                <th data-etiqueta="table_Cantidad">Cantidad</th>
                                <th data-etiqueta="table_UltimoPrecio">Último Precio</th>
                                <th data-etiqueta="table_ValorNominal">Valor Nominal</th>
                                <th data-etiqueta="table_Rendimiento">Rendimiento</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var compra in Compras)
                                { %>
                            <% foreach (var detalle in compra.Detalle)
                                { %>
                            <tr>
                                <td><%= detalle.Activo.Nombre %></td>
                                <td><%= detalle.Cantidad %></td>

                                <% if (detalle.Activo is Accion accion)
                                    { %>
                                <td><%= accion.Precio.ToString("N2") %></td>
                                <td>-</td>
                                <td><%= (detalle.Cantidad * accion.Precio).ToString("N2") %></td>
                                <% }
                                else if (detalle.Activo is Bono bono)
                                { %>
                                <td>-</td>
                                <td><%= bono.ValorNominal.ToString("N2") %></td>
                                <td><%= (detalle.Cantidad * bono.ValorNominal).ToString("N2") %></td>
                                <% } %>
                            </tr>
                            <% } %>
                            <% } %>
                        </tbody>
                    </table>

                    <div class="disponible-operar">
                        <h3 class="disponible-titulo" data-etiqueta="text_Efectivo">Efectivo</h3>
                        <p class="disponible-item">
                            <span class="icono-moneda">$</span>
                            <span data-etiqueta="text_Pesos">PESOS</span>
                            <span class="disponible-texto" data-etiqueta="text_DisponibleParaOperar">Disponible para operar</span>
                            <span class="disponible-monto">$ <%= PesosDisponibles.ToString("N2") %></span>
                        </p>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="custom-card resumen-card mb-3">
                    <div class="custom-card-header" data-etiqueta="text_Resumen">Resumen</div>
                    <div class="custom-card-body">
                        <p><strong data-etiqueta="text_ActivosValorizados">Activos Valorizados:</strong> <strong>$<%= ActivosValorizados.ToString("N2") %></strong></p>
                    </div>
                </div>

                <div class="custom-card distribucion-card">
                    <div class="custom-card-header" data-etiqueta="chart_DistribucionActivos">Distribución de Activos</div>
                    <div class="custom-card-body">
                        <canvas id="chartDistribucionActivos"></canvas>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            var ctx = document.getElementById('chartDistribucionActivos').getContext('2d');

            var accionesTotal = <%= AccionesTotalJson %>;
            var bonosTotal = <%= BonosTotalJson %>;

            var labels = [traducciones["chart_Acciones"], traducciones["chart_Bonos"]];
            var data = [accionesTotal, bonosTotal];

            var chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [
                        {
                            label: traducciones["chart_DistribucionActivos"],
                            data: data,
                            backgroundColor: ['#7986CB', '#A5D6A7'],
                            borderColor: ['#7986CB', '#A5D6A7'],
                            borderWidth: 1
                        }
                    ]
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
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    var label = context.label || '';
                                    if (label) {
                                        label += ': ';
                                    }
                                    label += context.raw.toLocaleString("es-AR", { style: "currency", currency: "ARS" });
                                    return label;
                                }
                            }
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>
