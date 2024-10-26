using System;
using System.Text.RegularExpressions;

namespace MidMarket.UI
{
    public partial class CargarSaldo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCargarSaldo_Click(object sender, EventArgs e)
        {
            //string numTarjeta = numeroTarjeta.Value;
            //string mensajeValidacion = ValidarNumeroTarjeta(numTarjeta);

            //if (!string.IsNullOrEmpty(mensajeValidacion))
            //{
            //    //lblCardType.Text = mensajeValidacion;
            //}
            //else
            //{
            //    lblResultado.Text = "Saldo cargado correctamente.";
            //}
        }

        private string ValidarNumeroTarjeta(string numeroTarjeta)
        {
            if (Regex.IsMatch(numeroTarjeta, @"^4[0-9]{12}(?:[0-9]{3})?$"))
            {
                return "Tarjeta VISA válida";
            }
            else if (Regex.IsMatch(numeroTarjeta, @"^5[1-5][0-9]{14}$"))
            {
                return "Tarjeta MasterCard válida";
            }
            else if (Regex.IsMatch(numeroTarjeta, @"^3[47][0-9]{13}$"))
            {
                return "Tarjeta AMEX válida";
            }
            else
            {
                return "Número de tarjeta inválido.";
            }
        }
    }
}
