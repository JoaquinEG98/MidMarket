using MidMarket.Business.Interfaces;
using MidMarket.Entities.Observer;
using MidMarket.UI.Helpers;
using System;
using System.Data.SqlClient;
using Unity;

namespace MidMarket.UI
{
    public partial class CargarSaldo : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public CargarSaldo()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCargarSaldo_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                if (!Page.IsValid)
                    return;

                string numTarjeta = numeroTarjeta.Text;
                string dni = dniTitular.Text;
                string fechaVencimientoTarjeta = fechaVencimiento.Text;
                decimal montoACargar = Convert.ToDecimal(monto.Text);

                _usuarioService.CargarSaldo(montoACargar, numTarjeta, dni, fechaVencimientoTarjeta);

                LimpiarCampos();

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_16")}");
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

        private void LimpiarCampos()
        {
            nombreTitular.Text = string.Empty;
            dniTitular.Text = string.Empty;
            numeroTarjeta.Text = string.Empty;
            fechaVencimiento.Text = string.Empty;
            codigoSeguridad.Text = string.Empty;
            monto.Text = string.Empty;

            lblDniValido.Text = string.Empty;
            lblCardType.Text = string.Empty;
            cardIcon.Attributes["src"] = string.Empty;
            cardIcon.Style["display"] = "none";
        }
    }
}
