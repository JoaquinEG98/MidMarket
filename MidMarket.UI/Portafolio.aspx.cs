using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class Portafolio : System.Web.UI.Page
    {
        private readonly ICompraService _compraService;
        private readonly ISessionManager _sessionManager;

        public decimal PesosDisponibles { get; set; }
        public List<TransaccionCompra> Compras { get; set; }
        public string AccionesTotalJson { get; set; }
        public string BonosTotalJson { get; set; }
        public decimal ActivosValorizados { get; set; }

        public Portafolio()
        {
            _compraService = Global.Container.Resolve<ICompraService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.VisualizarPortafolio))
                Response.Redirect("Default.aspx");

            try
            {
                PesosDisponibles = clienteLogueado.Cuenta.Saldo;
                CargarCompras();
                CargarGraficos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}");
                Response.Redirect("Default.aspx");
            }
        }

        private void CargarGraficos()
        {
            var activosAgrupados = AgruparActivos();
            ConsolidarActivos(activosAgrupados);
        }

        private Dictionary<string, (decimal Cantidad, decimal Precio, decimal ValorNominal)> AgruparActivos()
        {
            var activosAgrupados = new Dictionary<string, (decimal Cantidad, decimal Precio, decimal ValorNominal)>();

            foreach (var compra in Compras)
            {
                foreach (var detalle in compra.Detalle)
                {
                    string nombreActivo = detalle.Activo.Nombre;

                    if (detalle.Activo is Accion accion)
                    {
                        if (activosAgrupados.ContainsKey(nombreActivo))
                        {
                            var (cantidad, precio, valorNominal) = activosAgrupados[nombreActivo];
                            activosAgrupados[nombreActivo] = (cantidad + detalle.Cantidad, precio + (accion.Precio * detalle.Cantidad), valorNominal);
                        }
                        else
                        {
                            activosAgrupados[nombreActivo] = (detalle.Cantidad, accion.Precio * detalle.Cantidad, 0);
                        }
                    }
                    else if (detalle.Activo is Bono bono)
                    {
                        if (activosAgrupados.ContainsKey(nombreActivo))
                        {
                            var (cantidad, precio, valorNominal) = activosAgrupados[nombreActivo];
                            activosAgrupados[nombreActivo] = (cantidad + detalle.Cantidad, precio, valorNominal + (bono.ValorNominal * detalle.Cantidad));
                        }
                        else
                        {
                            activosAgrupados[nombreActivo] = (detalle.Cantidad, 0, bono.ValorNominal * detalle.Cantidad);
                        }
                    }
                }
            }

            return activosAgrupados;
        }

        private void ConsolidarActivos(Dictionary<string, (decimal Cantidad, decimal Precio, decimal ValorNominal)> activosAgrupados)
        {
            var activosConsolidados = new List<DetalleCompra>();

            foreach (var activo in activosAgrupados)
            {
                var detalleCompra = new DetalleCompra
                {
                    Activo = new Activo { Nombre = activo.Key },
                    Cantidad = (int)activo.Value.Cantidad
                };

                if (activo.Value.Precio > 0)
                {
                    detalleCompra.Activo = new Accion
                    {
                        Nombre = activo.Key,
                        Precio = activo.Value.Precio / activo.Value.Cantidad
                    };
                }
                else
                {
                    detalleCompra.Activo = new Bono
                    {
                        Nombre = activo.Key,
                        ValorNominal = activo.Value.ValorNominal / activo.Value.Cantidad
                    };
                }

                activosConsolidados.Add(detalleCompra);
            }

            Compras = new List<TransaccionCompra> { new TransaccionCompra { Detalle = activosConsolidados } };

            decimal accionesTotal = activosConsolidados
                .Where(d => d.Activo is Accion)
                .Sum(d => d.Cantidad * ((Accion)d.Activo).Precio);

            decimal bonosTotal = activosConsolidados
                .Where(d => d.Activo is Bono)
                .Sum(d => d.Cantidad * ((Bono)d.Activo).ValorNominal);

            ActivosValorizados = accionesTotal + bonosTotal;

            AccionesTotalJson = JsonConvert.SerializeObject(accionesTotal);
            BonosTotalJson = JsonConvert.SerializeObject(bonosTotal);
        }

        private void CargarCompras()
        {
            Compras = _compraService.GetCompras(false);
        }
    }
}
