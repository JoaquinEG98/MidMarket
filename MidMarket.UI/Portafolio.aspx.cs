using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

                // Datos separados para Acciones y Bonos
                decimal accionesTotal = 0M;
                decimal bonosTotal = 0M;

                foreach (var compra in Compras)
                {
                    foreach (var detalle in compra.Detalle)
                    {
                        if (detalle.Activo is Accion accion)
                        {
                            accionesTotal += detalle.Cantidad * accion.Precio;
                        }
                        else if (detalle.Activo is Bono bono)
                        {
                            bonosTotal += detalle.Cantidad * bono.ValorNominal;
                        }
                    }
                }

                // Convertir los valores a JSON
                AccionesTotalJson = JsonConvert.SerializeObject(accionesTotal);
                BonosTotalJson = JsonConvert.SerializeObject(bonosTotal);
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}");
                Response.Redirect("Default.aspx");
            }
        }

        private void CargarCompras()
        {
            Compras = _compraService.GetCompras();
        }
    }
}
