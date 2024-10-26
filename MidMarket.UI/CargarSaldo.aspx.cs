using MidMarket.Business.Interfaces;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class CargarSaldo : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;

        public CargarSaldo()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCargarSaldo_Click(object sender, EventArgs e)
        {
            try
            {
                string numTarjeta = numeroTarjeta.Text;
                string dni = dniTitular.Text;
                string fechaVencimientoTarjeta = fechaVencimiento.Text;
                decimal montoACargar = Convert.ToDecimal(monto.Text);

                _usuarioService.CargarSaldo(montoACargar, numTarjeta, dni, fechaVencimientoTarjeta);

                LimpiarCampos();

                AlertHelper.MostrarModal(this, $"Saldo cargado correctamente.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar saldo: {ex.Message}.");
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
