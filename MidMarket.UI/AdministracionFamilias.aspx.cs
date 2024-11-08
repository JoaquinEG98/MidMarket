using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Unity;

namespace MidMarket.UI
{
    public partial class AdministracionFamilias : System.Web.UI.Page
    {
        private readonly IPermisoService _permisoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public IList<Familia> Familias { get; set; }

        public AdministracionFamilias()
        {
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AdministracionFamilias))
                Response.Redirect("Default.aspx");

            try
            {
                Familias = _permisoService.GetFamilias();
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