using System;

namespace MidMarket.UI
{
    public partial class ValidarAcciones : System.Web.UI.UserControl
    {
        public string Nombre
        {
            get => txtNombre.Text;
            set => txtNombre.Text = value;
        }

        public string Simbolo
        {
            get => txtSimbolo.Text;
            set => txtSimbolo.Text = value;
        }

        public decimal Precio
        {
            get => decimal.TryParse(txtPrecio.Text, out var precio) ? precio : 0m;
            set => txtPrecio.Text = value.ToString("F2");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtSimbolo.Text = "";
            txtPrecio.Text = "";
        }
    }
}