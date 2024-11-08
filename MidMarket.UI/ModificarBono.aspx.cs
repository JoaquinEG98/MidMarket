using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Data.SqlClient;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class ModificarBono : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public Bono Bono { get; set; }
        private int _bonoId;

        public ModificarBono()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ModificarBono))
                Response.Redirect("Default.aspx");

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (!IsPostBack)
            {
                try
                {
                    _bonoId = int.Parse(Request.QueryString["id"]);
                    ViewState["BonoId"] = _bonoId;
                    CargarBono();
                }
                catch (SqlException)
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"{ex.Message}");
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

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                string nombreBono = ValidarBonos.Nombre;
                decimal valorNominal = decimal.Parse(ValidarBonos.ValorNominal);
                float tasaInteres = float.Parse(ValidarBonos.TasaInteres);

                GuardarBono(nombreBono, valorNominal, tasaInteres);
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_27")} {nombreBono}");
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        private void GuardarBono(string nombreBono, decimal valorNominal, float tasaInteres)
        {
            var bono = _activoService.GetBonos().FirstOrDefault(x => x.Id == _bonoId);

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (bono == null)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_28")}");
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
                Response.Redirect("AdministrarBonos.aspx");
                return;
            }

            ValidarBonos.Nombre = Bono.Nombre;
            ValidarBonos.ValorNominal = Bono.ValorNominal.ToString("F2");
            ValidarBonos.TasaInteres = Bono.TasaInteres.ToString("F2");
        }
    }
}
