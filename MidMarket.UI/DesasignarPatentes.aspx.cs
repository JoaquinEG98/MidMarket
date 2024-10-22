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
    public partial class DesasignarPatentes : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public IList<Componente> PatentesAsignadas { get; set; } = new List<Componente>();
        public int UsuarioSeleccionadoId { get; set; }
        public Cliente ClienteSeleccionado { get; set; }

        public DesasignarPatentes()
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
                        CargarPatentes(UsuarioSeleccionadoId);
                    }
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}.");
                Response.Redirect("Default.aspx");
            }
        }

        private void CargarPatentes(int usuarioId)
        {
            ClienteSeleccionado = _usuarioService.GetCliente(usuarioId);
            PatentesAsignadas = ClienteSeleccionado.Permisos.Where(p => !p.Hijos.Any()).ToList();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                var patentesSeleccionadas = Request.Form["patentesSeleccionadas"];
                var idsPatentesSeleccionadas = patentesSeleccionadas?.Split(',').Select(int.Parse).ToList() ?? new List<int>();

                ClienteSeleccionado.EliminarPermisosPorId(idsPatentesSeleccionadas);

                _permisoService.GuardarPermiso(ClienteSeleccionado);

                CargarPatentes(UsuarioSeleccionadoId);

                AlertHelper.MostrarModal(this, "Patentes eliminadas correctamente.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al eliminar patentes: {ex.Message}");
            }
        }
    }
}