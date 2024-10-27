using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using Unity;

namespace MidMarket.UI
{
    public partial class AdministrarBonos : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;

        public IList<Bono> Bonos { get; set; }

        public AdministrarBonos()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ModificarBono))
                Response.Redirect("Default.aspx");

            try
            {
                Bonos = _activoService.GetBonos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}.");
            }
        }
    }
}