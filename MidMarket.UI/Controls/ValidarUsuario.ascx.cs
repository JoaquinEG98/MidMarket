using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace MidMarket.UI
{
    public partial class ValidarUsuario : System.Web.UI.UserControl
    {
        public string EmailValue => txtEmailUsuario.Text;
        public string PasswordValue => txtPasswordUsuario.Text;
        public string RazonSocialValue => txtRazonSocialUsuario.Text;
        public string CUITValue => txtCUITUsuario.Text;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ValidatePassword(object source, ServerValidateEventArgs args)
        {
            string password = args.Value;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            args.IsValid = Regex.IsMatch(password, pattern);
        }

        public void LimpiarCampos()
        {
            txtEmailUsuario.Text = "";
            txtPasswordUsuario.Text = "";
            txtRazonSocialUsuario.Text = "";
            txtCUITUsuario.Text = "";
        }
    }
}