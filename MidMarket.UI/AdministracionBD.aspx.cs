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

        protected void btnBrowseRutaBackup_Click(object sender, EventArgs e)
        {
            // Lógica para abrir un diálogo de selección de archivos puede implementarse con JavaScript o ActiveX (si es necesario)
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
        }

        protected void btnRecalcularDigitos_Click(object sender, EventArgs e)
        {
        }
    }
}
