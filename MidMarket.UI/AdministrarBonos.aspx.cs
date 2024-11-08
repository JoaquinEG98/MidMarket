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
    public partial class AdministrarBonos : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public IList<Bono> Bonos { get; set; }

        public AdministrarBonos()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ModificarBono))
                Response.Redirect("Default.aspx");

            try
            {
                Bonos = _activoService.GetBonos();
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
    }
}