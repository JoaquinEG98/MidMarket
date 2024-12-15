using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace MidMarket.UI
{
    public partial class ValidarUsuario : System.Web.UI.UserControl
    {
        public string EmailValue
        {
            get => txtEmailUsuario.Text;
            set => txtEmailUsuario.Text = value;
        }

        public string PasswordValue
        {
            get => txtPasswordUsuario.Text;
            set => txtPasswordUsuario.Text = value;
        }

        public string RazonSocialValue
        {
            get => txtRazonSocialUsuario.Text;
            set => txtRazonSocialUsuario.Text = value;
        }

        public string CUITValue
        {
            get => txtCUITUsuario.Text;
            set => txtCUITUsuario.Text = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ValidatePassword(object source, ServerValidateEventArgs args)
        {
            string password = args.Value;
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            args.IsValid = Regex.IsMatch(password, pattern);
        }

        public void MostrarPassword(bool mostrar)
        {
            lblPassword.Visible = false;
            txtPasswordUsuario.Visible = mostrar;
            cvPassword.Enabled = mostrar;
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