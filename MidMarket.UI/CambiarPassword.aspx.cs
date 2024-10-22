using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MidMarket.UI
{
    public partial class CambiarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {


                AlertHelper.MostrarMensaje(this, $"Contraseña cambiada con éxito");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al dar de alta la Acción: {ex.Message}.");
            }
        }
    }
}