using Antlr.Runtime.Misc;
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
            try
            {

                Cliente cliente = new Cliente()
                {
                    Email = emailUsuario.Value,
                    Password = passwordUsuario.Value,
                    RazonSocial = razonSocialUsuario.Value,
                    CUIT = cuitUsuario.Value,
                };

                _usuarioService.RegistrarUsuario(cliente);

                AlertHelper.MostrarMensaje(this, $"Usuario dado de alta correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al dar de alta el Usuario: {ex.Message}.");
            }
        }

        private void LimpiarCampos()
        {
            emailUsuario.Value = "";
            passwordUsuario.Value = "";
            razonSocialUsuario.Value = "";
            cuitUsuario.Value = "";
        }
    }
}