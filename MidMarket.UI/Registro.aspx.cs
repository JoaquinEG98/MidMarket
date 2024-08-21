using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Response;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class Registro : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;

        public Registro()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                if (!EsTurnstileValido())
                {
                    lblError.Text = "Error: la verificación del CAPTCHA falló.";
                    lblError.Visible = true;
                    return;
                }

                Cliente cliente = new Cliente()
                {
                    Email = txtEmail.Value,
                    Password = txtPassword.Value,
                    RazonSocial = txtRazonSocial.Value,
                    CUIT = txtCUIT.Value,
                };

                _usuarioService.RegistrarUsuario(cliente);

                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = $"Error al registrar cliente: {ex.Message}";
                lblError.Visible = true;
            }
        }

        private bool EsTurnstileValido()
        {
            string secretKey = "0x4AAAAAAAhq4QxRRKDYzh-d84vc4DiZiJU";
            string captchaResponse = Request.Form["cf-turnstile-response"];

            using (var client = new System.Net.WebClient())
            {
                var postData = new System.Collections.Specialized.NameValueCollection();
                postData["secret"] = secretKey;
                postData["response"] = captchaResponse;

                byte[] response = client.UploadValues("https://challenges.cloudflare.com/turnstile/v0/siteverify", "POST", postData);
                string result = System.Text.Encoding.UTF8.GetString(response);

                var captchaResult = Newtonsoft.Json.JsonConvert.DeserializeObject<CaptchaResponse>(result);
                return captchaResult.Success;
            }
        }
    }
}