using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class AsignarPatentes : System.Web.UI.Page
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPermisoService _permisoService;
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public IList<Componente> PatentesAsignadas { get; set; } = new List<Componente>();
        public IList<Patente> PatentesDisponibles { get; set; } = new List<Patente>();
        public int UsuarioSeleccionadoId { get; set; }
        public Cliente ClienteSeleccionado { get; set; }

        public AsignarPatentes()
        {
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clientes = _usuarioService.GetClientes();
            }
            else
            {
                // Recuperar el ID del usuario desde el Request o desde el ViewState
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
                // Capturar los IDs de las patentes asignadas y seleccionadas
                var patentesSeleccionadas = Request.Form["patentesSeleccionadas"];
                var patentesAsignadas = Request.Form["patentesAsignadas"];

                // Convertir los IDs en listas para procesarlos
                var idsPatentesSeleccionadas = patentesSeleccionadas?.Split(',').Select(int.Parse).ToList() ?? new List<int>();
                var idsPatentesAsignadas = patentesAsignadas?.Split(',').Select(int.Parse).ToList() ?? new List<int>();

                // Obtener las patentes seleccionadas a partir de los IDs
                var nuevasPatentes = _permisoService.GetPatentes()
                    .Where(p => idsPatentesSeleccionadas.Contains(p.Id))
                    .ToList();

                // Agregar las nuevas patentes a la lista de permisos del cliente seleccionado
                ClienteSeleccionado.Permisos.AddRange(nuevasPatentes);

                // Aquí se asume que `GuardarPermiso` actualiza al cliente y sus permisos en la base de datos
                _permisoService.GuardarPermiso(ClienteSeleccionado);

                // Mostrar mensaje de éxito
                AlertHelper.MostrarMensaje(this, "Patentes asignadas correctamente.");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                AlertHelper.MostrarMensaje(this, $"Error al asignar patentes: {ex.Message}");
            }
        }
    }
}