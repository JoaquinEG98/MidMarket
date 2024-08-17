using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MidMarket.UI.Helpers
{
    public static class AlertHelper
    {
        public static void MostrarMensaje(Page page, string mensaje)
        {
            string script = $"alert('{mensaje}');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alerta", script, true);
        }
    }
}