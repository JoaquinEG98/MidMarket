using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MidMarket.UI
{
    public partial class Toast : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void MostrarToast(string mensaje)
        {
            toastMessageLiteral.Text = mensaje;
            hfShowToast.Value = "true";
        }
    }
}