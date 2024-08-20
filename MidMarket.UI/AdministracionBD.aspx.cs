using MidMarket.Business.Interfaces;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class AdministracionBD : System.Web.UI.Page
    {
        private readonly IBackupService _backupService;

        public AdministracionBD()
        {
            _backupService = Global.Container.Resolve<IBackupService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaBackup = txtRutaBackup.Text.Trim();

                if (string.IsNullOrEmpty(rutaBackup))
                {
                    AlertHelper.MostrarMensaje(this, $"Debe seleccionar una ruta válida para guardar el backup.");
                    return;
                }

                _backupService.RealizarBackup(rutaBackup);
                AlertHelper.MostrarMensaje(this, $"Backup realizado con éxito");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al generar backup: {ex.Message}.");
            }
        }

        protected void btnRestaurarBD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fileUploadRestore.HasFile)
                {
                    AlertHelper.MostrarMensaje(this, "Debe seleccionar un archivo de backup.");
                    return;
                }

                string rutaBackup = Server.MapPath("~/App_Data/") + fileUploadRestore.FileName;

                fileUploadRestore.SaveAs(rutaBackup);

                _backupService.RealizarRestore(rutaBackup);

                AlertHelper.MostrarMensaje(this, "Restauración realizada con éxito.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al restaurar la base de datos: {ex.Message}.");
            }
        }

        protected void btnRecalcularDigitos_Click(object sender, EventArgs e)
        {
        }
    }
}
