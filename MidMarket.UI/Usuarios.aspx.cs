using MidMarket.Business.Interfaces;
using MidMarket.Entities.Composite;
using MidMarket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;
using MidMarket.UI.Helpers;
using Newtonsoft.Json;

namespace MidMarket.UI
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                    CargarClientes();
                
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
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