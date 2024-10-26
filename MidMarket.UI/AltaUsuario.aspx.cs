using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaUsuario : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;

        public AltaUsuario()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    Cliente cliente = new Cliente()
                    {
                        Email = ValidarUsuarioControl.EmailValue,
                        Password = ValidarUsuarioControl.PasswordValue,
                        RazonSocial = ValidarUsuarioControl.RazonSocialValue,
                        CUIT = ValidarUsuarioControl.CUITValue
                    };

                    _usuarioService.RegistrarUsuario(cliente);

                    AlertHelper.MostrarModal(this, $"Usuario dado de alta correctamente.");
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    AlertHelper.MostrarModal(this, $"Error al dar de alta el Usuario: {ex.Message}.");
                }
            }
        }

        private void LimpiarCampos()
        {
            ValidarUsuarioControl.LimpiarCampos();
        }
    }
}