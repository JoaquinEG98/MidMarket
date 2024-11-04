using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaBonos : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public AltaBonos()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AltaBono))
                Response.Redirect("Default.aspx");
        }

        protected void btnCargarBono_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                var idioma = _sessionManager.Get<IIdioma>("Idioma");

                Bono bono = new Bono
                {
                    Nombre = ValidarBonos.Nombre,
                    ValorNominal = decimal.Parse(ValidarBonos.ValorNominal),
                    TasaInteres = float.Parse(ValidarBonos.TasaInteres)
                };
                _activoService.AltaBono(bono);

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_07")}");
                ValidarBonos.LimpiarCampos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }
    }
}