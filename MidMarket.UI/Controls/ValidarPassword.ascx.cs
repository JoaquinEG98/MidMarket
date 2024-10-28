using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace MidMarket.UI
{
    public partial class ValidarPassword : System.Web.UI.UserControl
    {
        public string PasswordValue => txtPassword.Text;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ValidatePassword(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ValidarFormatoPassword(args.Value);
        }

        private bool ValidarFormatoPassword(string password)
        {
            string regex = @"^(?=.*[A-Z])(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, regex);
        }
    }
}