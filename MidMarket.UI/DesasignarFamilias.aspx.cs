using MidMarket.Business.Interfaces;
using MidMarket.Entities.Composite;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class DesasignarFamilias : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public IList<Componente> FamiliasAsignadas { get; set; } = new List<Componente>();
        public int UsuarioSeleccionadoId { get; set; }
        public Cliente ClienteSeleccionado { get; set; }

        public DesasignarFamilias()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Clientes = _usuarioService.GetClientes();

                if (IsPostBack)
                {
                    string filtroUsuario = Request.Form["filtroUsuario"];
                    ViewState["FiltroUsuario"] = filtroUsuario;

                    if (int.TryParse(Request.Form["usuarioSeleccionado"], out int usuarioId))
                    {
                        UsuarioSeleccionadoId = usuarioId;
                        ViewState["UsuarioSeleccionadoId"] = usuarioId;
                    }
                    else if (ViewState["UsuarioSeleccionadoId"] != null)
                    {
                        UsuarioSeleccionadoId = (int)ViewState["UsuarioSeleccionadoId"];
                    }

                    if (UsuarioSeleccionadoId > 0)
                    {
                        CargarFamilias(UsuarioSeleccionadoId);
                    }
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
                Response.Redirect("Default.aspx");
            }
        }

        private void CargarFamilias(int usuarioId)
        {
            ClienteSeleccionado = _usuarioService.GetCliente(usuarioId);
            FamiliasAsignadas = ClienteSeleccionado.Permisos.Where(f => f.Hijos.Any()).ToList();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var familiasSeleccionadas = Request.Form["familiasSeleccionadas"];
                var idsFamiliasSeleccionadas = familiasSeleccionadas?.Split(',').Select(int.Parse).ToList() ?? new List<int>();

                ClienteSeleccionado.EliminarPermisosPorId(idsFamiliasSeleccionadas);

                _permisoService.GuardarPermiso(ClienteSeleccionado);

                CargarFamilias(UsuarioSeleccionadoId);

                AlertHelper.MostrarMensaje(this, "Familias eliminadas correctamente.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al eliminar familias: {ex.Message}");
            }
        }
    }
}