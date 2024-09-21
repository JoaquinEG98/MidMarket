using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MidMarket.UI.Helpers
{
    public static class AlertHelper
    {
        public static void MostrarAlert(Page page, string mensaje)
        {
            string script = $"alert('{mensaje}');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alerta", script, true);
        }

        public static void MostrarMensaje(Page page, string mensaje, bool redirigir = false)
        {
            var modalControl = (Modal)page.Master.FindControl("globalModalControl");
            if (modalControl != null)
            {
                var literalMensaje = (Literal)modalControl.FindControl("modalMessageLiteral");
                if (literalMensaje != null)
                {
                    literalMensaje.Text = mensaje;
                }

                var hfShowModal = (HiddenField)modalControl.FindControl("hfShowModal");
                var hfRedirigir = (HiddenField)modalControl.FindControl("hfRedirigir");

                if (hfShowModal != null)
                {
                    hfShowModal.Value = "true";  // Establece que se debe mostrar el modal
                }

                if (redirigir && hfRedirigir != null)
                {
                    hfRedirigir.Value = "true";  // Establece si debe redirigir
                }
            }
        }
    }
}