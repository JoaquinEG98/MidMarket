﻿using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
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
        protected bool esAdmin = false;


        private readonly ISessionManager _sessionManager;
        private readonly IUsuarioService _usuarioService;
        private readonly ICompraService _compraService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly ITraduccionService _traduccionService;

        public _Default()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _compraService = Global.Container.Resolve<ICompraService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Cliente = _sessionManager.Get<Cliente>("Usuario");

            if (Cliente == null)
                Response.Redirect("Default.aspx");

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                VerificarDV();

                esAdmin = Cliente.Permisos.Any(permiso => permiso.Nombre == "Webmaster" || permiso.Nombre == "Administrador Financiero");

                if (!esAdmin)
                {
                    TotalInvertido = _usuarioService.ObtenerTotalInvertido();
                    UltimaTransaccion = _usuarioService.ObtenerUltimaTransaccion();
                    LlenarInformacionGrafico();
                }

                LlenarFamiliaUsuario();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_23")}", true);
                _sessionManager.AbandonSession();
            }
        }

        private void VerificarDV()
        {
            var tablas = new List<string>();

            bool consistencia = _digitoVerificadorService.VerificarInconsistenciaTablas(out tablas);
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

                VerificarWebmaster(esWebmaster, tablas);
            }
        }

        private void VerificarWebmaster(bool esWebmaster, List<string> tablas)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (!esWebmaster)
            {
                Response.Redirect("Error.aspx");
            }
            else
            {
                string mensaje;

                if (tablas.Count == 1)
                {
                    mensaje = $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_24")} {tablas[0]}";
                }
                else
                {
                    string tablasInconsistentes = string.Join(", ", tablas);
                    mensaje = $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_24")} {tablasInconsistentes}";
                }

                AlertHelper.MostrarModal(this, mensaje);
            }
        }

        private void LlenarInformacionGrafico()
        {
            var compras = _compraService.GetCompras(true);
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
