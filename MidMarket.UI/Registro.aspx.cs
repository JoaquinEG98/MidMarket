using MidMarket.Business.Interfaces;
using MidMarket.Entities;
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
                Cliente cliente = new Cliente()
                {
                    Email = txtEmail.Value,
                    Password = txtPassword.Value,
                    RazonSocial = txtRazonSocial.Value,
                    CUIT = txtCUIT.Value,
                };

                _usuarioService.RegistrarUsuario(cliente);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}