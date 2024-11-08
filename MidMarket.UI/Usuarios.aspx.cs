using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Unity;

namespace MidMarket.UI
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public string ClientesJson
        {
            get
            {
                return JsonConvert.SerializeObject(Clientes, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }

        public Usuarios()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AdministracionUsuarios))
                Response.Redirect("Default.aspx");

            try
            {
                CargarClientes();
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
                Response.Redirect("Default.aspx");
            }
        }

        private void CargarClientes()
        {
            Clientes = _usuarioService.GetClientes();
            ViewState["ClientesJson"] = JsonConvert.SerializeObject(Clientes, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}