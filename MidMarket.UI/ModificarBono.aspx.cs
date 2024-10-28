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
        private int _bonoId;

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

            if (!IsPostBack)
            {
                try
                {
                    _bonoId = int.Parse(Request.QueryString["id"]);
                    ViewState["BonoId"] = _bonoId;
                    CargarBono();
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}");
                    Response.Redirect("AdministrarBonos.aspx");
                }
            }
            else
            {
                _bonoId = (int)ViewState["BonoId"];
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                string nombreBono = ValidarBonos.Nombre;
                decimal valorNominal = decimal.Parse(ValidarBonos.ValorNominal);
                float tasaInteres = float.Parse(ValidarBonos.TasaInteres);

                GuardarBono(nombreBono, valorNominal, tasaInteres);
                AlertHelper.MostrarModal(this, $"Bono {nombreBono} modificado correctamente.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al modificar el bono: {ex.Message}");
            }
        }

        private void GuardarBono(string nombreBono, decimal valorNominal, float tasaInteres)
        {
            var bono = _activoService.GetBonos().FirstOrDefault(x => x.Id == _bonoId);

            if (bono == null)
            {
                AlertHelper.MostrarModal(this, "No se pudo cargar el bono para guardar los cambios.");
                return;
            }

            bono.Nombre = nombreBono;
            bono.ValorNominal = valorNominal;
            bono.TasaInteres = tasaInteres;

            _activoService.ModificarBono(bono);
        }

        private void CargarBono()
        {
            Bono = _activoService.GetBonos().FirstOrDefault(x => x.Id == _bonoId);
            if (Bono == null)
            {
                AlertHelper.MostrarModal(this, "Bono no encontrado.");
                Response.Redirect("AdministrarBonos.aspx");
                return;
            }

            ValidarBonos.Nombre = Bono.Nombre;
            ValidarBonos.ValorNominal = Bono.ValorNominal.ToString("F2");
            ValidarBonos.TasaInteres = Bono.TasaInteres.ToString("F2");
        }
    }
}
