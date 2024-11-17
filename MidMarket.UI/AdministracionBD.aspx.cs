using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Unity;

namespace MidMarket.UI
{
    public partial class AdministracionBD : System.Web.UI.Page
    {
        private readonly IBackupService _backupService;
        private readonly IDigitoVerificadorService _digitoVerificadorService;
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;
        private readonly ICompraService _compraService;
        private readonly IVentaService _ventaService;
        private readonly ICarritoService _carritoService;
        private readonly IBitacoraService _bitacoraService;
        private readonly IActivoService _activoService;

        public AdministracionBD()
        {
            _backupService = Global.Container.Resolve<IBackupService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
            _compraService = Global.Container.Resolve<ICompraService>();
            _ventaService = Global.Container.Resolve<IVentaService>();
            _carritoService = Global.Container.Resolve<ICarritoService>();
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
            _activoService = Global.Container.Resolve<IActivoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (clienteLogueado == null
                || (!clienteLogueado.Debug
                && !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AdministracionBaseDeDatos)))
                Response.Redirect("Default.aspx");

            try
            {
                if (!clienteLogueado.Debug)
                    CargarDV();
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        protected void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                string rutaBackup = txtRutaBackup.Text.Trim();

                if (string.IsNullOrEmpty(rutaBackup))
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_01")}");
                    return;
                }

                _backupService.RealizarBackup(rutaBackup);

                CargarDV();

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_02")}");
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        protected void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                if (!fileUploadRestore.HasFile)
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_03")}");
                    return;
                }

                string rutaBackup = Server.MapPath("~/App_Data/") + fileUploadRestore.FileName;

                fileUploadRestore.SaveAs(rutaBackup);

                _backupService.RealizarRestore(rutaBackup);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                if (clienteLogueado.Debug)
                {
                    _traduccionService.LimpiarCache();
                    _sessionManager.Remove("Usuario");
                    _sessionManager.AbandonSession();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    CargarDV();
                }

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_04")}");
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        protected void btnRecalcularDigitos_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                _digitoVerificadorService.RecalcularTodosDigitosVerificadores(_usuarioService, _permisoService, _compraService, _ventaService, _carritoService, _bitacoraService, _activoService);

                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
                if (clienteLogueado.Debug)
                {
                    _traduccionService.LimpiarCache();
                    _sessionManager.Remove("Usuario");
                    _sessionManager.AbandonSession();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    CargarDV();
                }

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_05")}");
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        private void CargarDV()
        {
            var tablas = new List<string>();
            bool consistencia = _digitoVerificadorService.VerificarInconsistenciaTablas(out tablas, _bitacoraService);
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (!consistencia)
            {
                if (tablas.Count == 1)
                {
                    estadoDVLiteral.Text = $"<span id='estadoDV' class='status-text incorrecto'>{_traduccionService.ObtenerMensaje(idioma, "MSJ_39")} - {tablas[0]}</span>";
                }
                else
                {
                    string tablasInconsistentes = string.Join(", ", tablas);
                    estadoDVLiteral.Text = $"<span id='estadoDV' class='status-text incorrecto'>{_traduccionService.ObtenerMensaje(idioma, "MSJ_39")} - {tablasInconsistentes}</span>";
                }
            }
            else
            {
                estadoDVLiteral.Text = $"<span id='estadoDV' class='status-text correcto'>{_traduccionService.ObtenerMensaje(idioma, "MSJ_38")}</span>";
            }
        }
    }
}
