using System;

namespace MidMarket.UI
{
    public partial class ValidarBonos : System.Web.UI.UserControl
    {
        public string Nombre
        {
            get => txtNombreBono.Text;
            set => txtNombreBono.Text = value;
        }

        public string ValorNominal
        {
            get => txtValorNominal.Text;
            set => txtValorNominal.Text = value;
        }

        public string TasaInteres
        {
            get => txtTasaInteres.Text;
            set => txtTasaInteres.Text = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void LimpiarCampos()
        {
            Nombre = "";
            ValorNominal = "";
            TasaInteres = "";
        }
    }
}