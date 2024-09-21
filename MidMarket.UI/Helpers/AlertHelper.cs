﻿using System;
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

        public static void MostrarMensaje(Page page, string mensaje)
        {
            var modalControl = (Modal)page.Master.FindControl("globalModalControl");
            if (modalControl != null)
            {
                modalControl.MostrarModal(mensaje);

                var hfShowModal = (HiddenField)page.Master.FindControl("hfShowModal");
                if (hfShowModal != null)
                {
                    hfShowModal.Value = "false";
                }
            }
        }
    }
}