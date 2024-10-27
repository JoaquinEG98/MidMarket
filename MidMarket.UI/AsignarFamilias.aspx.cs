using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class AsignarFamilias : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly ISessionManager _sessionManager;

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public IList<Componente> FamiliasAsignadas { get; set; } = new List<Componente>();
        public IList<Familia> FamiliasDisponibles { get; set; } = new List<Familia>();
        public int UsuarioSeleccionadoId { get; set; }
        public Cliente ClienteSeleccionado { get; set; }

        public AsignarFamilias()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AsignarFamilias))
                Response.Redirect("Default.aspx");

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
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}.");
                Response.Redirect("Default.aspx");
            }
        }

        private void CargarFamilias(int usuarioId)
        {
            ClienteSeleccionado = _usuarioService.GetCliente(usuarioId);
            FamiliasAsignadas = ClienteSeleccionado.Permisos.Where(f => f.Hijos.Any()).ToList();

            var todasLasFamilias = _permisoService.GetFamilias();
            FamiliasDisponibles = todasLasFamilias.Where(f => !FamiliasAsignadas.Any(fa => fa.Id == f.Id)).ToList();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var familiasSeleccionadas = Request.Form["familiasSeleccionadas"];
                var familiasAsignadas = Request.Form["familiasAsignadas"];

                var idsFamiliasSeleccionadas = familiasSeleccionadas?.Split(',').Select(int.Parse).ToList() ?? new List<int>();

                var nuevasFamilias = _permisoService.GetFamilias().Where(f => idsFamiliasSeleccionadas.Contains(f.Id)).ToList();

                ClienteSeleccionado.Permisos.AddRange(nuevasFamilias);

                _permisoService.GuardarPermiso(ClienteSeleccionado);

                CargarFamilias(ClienteSeleccionado.Id);

                AlertHelper.MostrarModal(this, "Familias asignadas correctamente.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al asignar familias: {ex.Message}");
            }
        }
    }
}