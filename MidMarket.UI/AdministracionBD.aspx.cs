using MidMarket.Business.Interfaces;
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

        public AdministracionBD()
        {
            _backupService = Global.Container.Resolve<IBackupService>();
            _digitoVerificadorService = Global.Container.Resolve<IDigitoVerificadorService>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();  
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDV();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar página: {ex.Message}.");
            }
        }

        protected void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaBackup = txtRutaBackup.Text.Trim();

                if (string.IsNullOrEmpty(rutaBackup))
                {
                    AlertHelper.MostrarModal(this, $"Debe seleccionar una ruta válida para guardar el backup.");
                    return;
                }

                _backupService.RealizarBackup(rutaBackup);

                CargarDV();

                AlertHelper.MostrarModal(this, $"Backup realizado con éxito");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al generar backup: {ex.Message}.");
            }
        }

        protected void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fileUploadRestore.HasFile)
                {
                    AlertHelper.MostrarModal(this, "Debe seleccionar un archivo de backup.");
                    return;
                }

                string rutaBackup = Server.MapPath("~/App_Data/") + fileUploadRestore.FileName;

                fileUploadRestore.SaveAs(rutaBackup);

                _backupService.RealizarRestore(rutaBackup);

                CargarDV();

                AlertHelper.MostrarModal(this, "Restauración realizada con éxito.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al restaurar la base de datos: {ex.Message}.");
            }
        }

        protected void btnRecalcularDigitos_Click(object sender, EventArgs e)
        {
            try
            {
                _digitoVerificadorService.RecalcularTodosDigitosVerificadores(_usuarioService, _permisoService);

                CargarDV();

                AlertHelper.MostrarModal(this, "Digitos verificadores recalculados con éxito");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al recalcular digitos verificadores: {ex.Message}.");
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
