using System;

namespace MidMarket.UI
{
    public partial class ValidarEmail : System.Web.UI.UserControl
    {
        public string Email
        {
            get => txtEmail.Text;
            set => txtEmail.Text = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}