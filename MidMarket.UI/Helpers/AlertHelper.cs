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

        public static void MostrarModal(Page page, string mensaje, bool redirigir = false)
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
                    hfShowModal.Value = "true";
                }

                if (redirigir && hfRedirigir != null)
                {
                    hfRedirigir.Value = "true";
                }
            }
        }

        public static void MostrarToast(Page page, string mensaje)
        {
            var toastControl = (Toast)page.Master.FindControl("globalToastControl");
            if (toastControl != null)
            {
                var literalMensaje = (Literal)toastControl.FindControl("toastMessageLiteral");
                if (literalMensaje != null)
                {
                    literalMensaje.Text = mensaje;
                }

                var hfShowToast = (HiddenField)toastControl.FindControl("hfShowToast");
                if (hfShowToast != null)
                {
                    hfShowToast.Value = "true";
                }
            }
        }
    }
}