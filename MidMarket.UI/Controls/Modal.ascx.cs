using System;

namespace MidMarket.UI
{
    public partial class Modal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void MostrarModal(string mensaje)
        {
            modalMessageLiteral.Text = mensaje;
            hfShowModal.Value = "true";
        }
    }
}