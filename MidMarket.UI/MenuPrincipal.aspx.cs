using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Unity;

namespace MidMarket.UI
{
    public partial class _Default : Page
    {
        public Cliente Cliente { get; set; }
        public string Familia { get; set; } = "Cliente";
        public decimal TotalInvertido { get; set; }
        public decimal UltimaTransaccion { get; set; }
        public string LabelsJson { get; set; }
        public string AccionesDataJson { get; set; }
        public string BonosDataJson { get; set; }

        private readonly ISessionManager _sessionManager;
        private readonly IUsuarioService _usuarioService;
        private readonly ICompraService _compraService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;

        public _Default()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _compraService = Global.Container.Resolve<ICompraService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Cliente = _sessionManager.Get<Cliente>("Usuario");

                    if (Cliente == null)
                        Response.Redirect("Default.aspx");

                    VerificarDV();

                    TotalInvertido = _usuarioService.ObtenerTotalInvertido();
                    UltimaTransaccion = _usuarioService.ObtenerUltimaTransaccion();

                    LlenarInformacionGrafico();
                    LlenarFamiliaUsuario();
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"Error al cargar la página. Será redirigido al inicio.", true);
                    _sessionManager.AbandonSession();
                }
            }
        }

        private void VerificarDV()
        {
            bool consistencia = _digitoVerificadorService.VerificarInconsistenciaTablas();
            bool esWebmaster = false;

            if (!consistencia)
            {
                foreach (var permiso in Cliente.Permisos)
                {
                    if (permiso.Permiso == Entities.Enums.Permiso.EsFamilia && permiso.Nombre == "Webmaster")
                    {
                        esWebmaster = true;
                    }
                }

                VerificarWebmaster(esWebmaster);
            }
        }

        private void VerificarWebmaster(bool esWebmaster)
        {
            if (!esWebmaster)
            {
                Response.Redirect("Error.aspx");
            }
            else
            {
                AlertHelper.MostrarModal(this, $"Inconsistencia en los digitos verificadores, por favor revise en la sección de Administración de Base de Datos.");
            }
        }

        private void LlenarInformacionGrafico()
        {
            var compras = _compraService.GetCompras();
            var labels = new List<string>();
            var accionesData = new List<decimal>();
            var bonosData = new List<decimal>();

            foreach (var compra in compras)
            {
                labels.Add(compra.Fecha.ToString("yyyy-MM-dd"));

                decimal accionesTotal = 0;
                decimal bonosTotal = 0;

                foreach (var detalle in compra.Detalle)
                {
                    if (detalle.Activo is Accion)
                    {
                        accionesTotal += detalle.Precio;
                    }
                    else if (detalle.Activo is Bono)
                    {
                        bonosTotal += detalle.Precio;
                    }
                }

                accionesData.Add(accionesTotal);
                bonosData.Add(bonosTotal);
            }

            LabelsJson = JsonConvert.SerializeObject(labels);
            AccionesDataJson = JsonConvert.SerializeObject(accionesData);
            BonosDataJson = JsonConvert.SerializeObject(bonosData);
        }

        private void LlenarFamiliaUsuario()
        {
            if (Cliente.Permisos.Count > 0)
            {
                var permisoFamilia = Cliente.Permisos.FirstOrDefault(x => x.Permiso == Entities.Enums.Permiso.EsFamilia);
                if (permisoFamilia != null)
                {
                    Familia = permisoFamilia.Nombre.ToString();
                }
            }
        }
    }
}
