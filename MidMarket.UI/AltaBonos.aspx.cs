using MidMarket.Business.Interfaces;
using MidMarket.Entities;
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

        public AltaBonos()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
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
                Bono bono = new Bono
                {
                    Nombre = ValidarBonos.Nombre,
                    ValorNominal = decimal.Parse(ValidarBonos.ValorNominal),
                    TasaInteres = float.Parse(ValidarBonos.TasaInteres)
                };
                _activoService.AltaBono(bono);

                AlertHelper.MostrarModal(this, $"Bono {bono.Nombre} dado de alta correctamente.");
                ValidarBonos.LimpiarCampos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al dar de alta la Acción: {ex.Message}.");
            }
        }
    }
}