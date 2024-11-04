using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
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

        public AdministracionBD()
        {
            _backupService = Global.Container.Resolve<IBackupService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AdministracionBaseDeDatos))
                Response.Redirect("Default.aspx");

            try
            {
                CargarDV();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        protected void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                var idioma = _sessionManager.Get<IIdioma>("Idioma");

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
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        protected void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            try
            {
                var idioma = _sessionManager.Get<IIdioma>("Idioma");

                if (!fileUploadRestore.HasFile)
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_03")}");
                    return;
                }

                string rutaBackup = Server.MapPath("~/App_Data/") + fileUploadRestore.FileName;

                fileUploadRestore.SaveAs(rutaBackup);

                _backupService.RealizarRestore(rutaBackup);

                CargarDV();

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_04")}");
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
                _digitoVerificadorService.RecalcularTodosDigitosVerificadores(_usuarioService, _permisoService);

                CargarDV();

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_05")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        private void CargarDV()
        {
            bool consistencia = _digitoVerificadorService.VerificarInconsistenciaTablas();

            if (!consistencia)
            {
                estadoDVLiteral.Text = "<span id='estadoDV' class='status-text incorrecto'>Estado: Incorrecto</span>";
            }
            else
            {
                estadoDVLiteral.Text = "<span id='estadoDV' class='status-text correcto'>Estado: Correcto</span>";
            }
        }
    }
}
