using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class ModificarBono : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;

        public Bono Bono { get; set; }
        private int _bonoId { get; set; }

        public ModificarBono()
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
                _bonoId = int.Parse(Request.QueryString["id"]);

                CargarBono();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}");
                Response.Redirect("AdministrarBonos.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreBono = Request.Form["nombreBono"];
                decimal valorNominal = decimal.Parse(Request.Form["valorNominal"]);
                float tasaInteres = float.Parse(Request.Form["tasaInteres"]);

                GuardarBono(nombreBono, valorNominal, tasaInteres);
                AlertHelper.MostrarModal(this, $"Bono {nombreBono} modificado correctamente.");
                CargarBono();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al modificar el bono: {ex.Message}");
            }
        }

        private void GuardarBono(string nombreBono, decimal valorNominal, float tasaInteres)
        {
            var bono = new Bono()
            {
                Id = Bono.Id,
                Id_Bono = Bono.Id_Bono,
                Nombre = nombreBono,
                ValorNominal = valorNominal,
                TasaInteres = tasaInteres
            };

            _activoService.ModificarBono(bono);
        }

        private void CargarBono()
        {
            Bono = _activoService.GetBonos().FirstOrDefault(x => x.Id == _bonoId);
            if (Bono == null)
            {
                AlertHelper.MostrarModal(this, "Bono no encontrado.");
                Response.Redirect("AdministrarBonos.aspx");
            }
        }
    }
}