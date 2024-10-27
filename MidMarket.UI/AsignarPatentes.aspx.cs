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
    public partial class AsignarPatentes : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        private readonly ISessionManager _sessionManager;

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public IList<Componente> PatentesAsignadas { get; set; } = new List<Componente>();
        public IList<Patente> PatentesDisponibles { get; set; } = new List<Patente>();
        public int ClienteSeleccionadoId { get; set; }
        public Cliente ClienteSeleccionado { get; set; }

        public AsignarPatentes()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AsignarPatentes))
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
                        ClienteSeleccionadoId = usuarioId;
                        ViewState["UsuarioSeleccionadoId"] = usuarioId;
                    }
                    else if (ViewState["UsuarioSeleccionadoId"] != null)
                    {
                        ClienteSeleccionadoId = (int)ViewState["UsuarioSeleccionadoId"];
                    }

                    if (ClienteSeleccionadoId > 0)
                    {
                        CargarPatentes(ClienteSeleccionadoId);
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

            var todasLasPatentes = _permisoService.GetPatentes();
            PatentesDisponibles = todasLasPatentes.Where(p => !PatentesAsignadas.Any(pa => pa.Id == p.Id)).ToList();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var patentesSeleccionadas = Request.Form["patentesSeleccionadas"];
                var patentesAsignadas = Request.Form["patentesAsignadas"];


                var idsPatentesSeleccionadas = patentesSeleccionadas?.Split(',').Select(int.Parse).ToList() ?? new List<int>();
                //var idsPatentesAsignadas = patentesAsignadas?.Split(',').Select(int.Parse).ToList() ?? new List<int>();

                var nuevasPatentes = _permisoService.GetPatentes().Where(p => idsPatentesSeleccionadas.Contains(p.Id)).ToList();

                ClienteSeleccionado.Permisos.AddRange(nuevasPatentes);

                _permisoService.GuardarPermiso(ClienteSeleccionado);

                CargarPatentes(ClienteSeleccionado.Id);

                AlertHelper.MostrarModal(this, "Patentes asignadas correctamente.");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al asignar patentes: {ex.Message}");
            }
        }
    }
}