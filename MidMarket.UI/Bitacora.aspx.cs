using MidMarket.Business.Interfaces;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class Bitacora : System.Web.UI.Page
    {
        private readonly IBitacoraService _bitacoraService;
        private readonly IUsuarioService _usuarioService;

        public List<Entities.Bitacora> Movimientos { get; set; } = new List<Entities.Bitacora>();

        private int PaginaActual
        {
            get
            {
                return ViewState["PaginaActual"] != null ? (int)ViewState["PaginaActual"] : 0;
            }
            set
            {
                ViewState["PaginaActual"] = value;
            }
        }

        private int TotalPaginas
        {
            get
            {
                return ViewState["TotalPaginas"] != null ? (int)ViewState["TotalPaginas"] : 1;
            }
            set
            {
                ViewState["TotalPaginas"] = value;
            }
        }

        protected int GetPaginaActual() => PaginaActual + 1; // Esto se hace 1-indexado para mostrar al usuario
        protected int GetTotalPaginas() => TotalPaginas;

        public Bitacora()
        {
            _bitacoraService = Global.Container.Resolve<IBitacoraService>();
            _usuarioService = Global.Container.Resolve<IUsuarioService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes(); // Cargar la lista de clientes en el DropDownList
                PaginaActual = 0;
                CargarBitacora();
            }
        }

        private void CargarClientes()
        {
            try
            {
                var clientes = _usuarioService.GetClientes(); // Obtener la lista de clientes
                ddlUsuario.Items.Clear(); // Limpiar los elementos actuales del DropDownList
                ddlUsuario.Items.Add(new ListItem("Todos", "")); // Opción para mostrar todos los clientes

                foreach (var cliente in clientes)
                {
                    ddlUsuario.Items.Add(new ListItem(cliente.RazonSocial, cliente.RazonSocial)); // Agregar los clientes al DropDownList
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la lista de clientes: {ex.Message}.");
            }
        }

        private void CargarBitacora()
        {
            try
            {
                var todosMovimientos = _bitacoraService.GetBitacora();

                // Filtrar por usuario
                if (!string.IsNullOrEmpty(ddlUsuario.SelectedValue))
                {
                    todosMovimientos = todosMovimientos.Where(m => m.Cliente.RazonSocial == ddlUsuario.SelectedValue).ToList();
                }

                // Filtrar por criticidad
                if (!string.IsNullOrEmpty(ddlCriticidad.SelectedValue))
                {
                    todosMovimientos = todosMovimientos.Where(m => m.Criticidad.ToString() == ddlCriticidad.SelectedValue).ToList();
                }

                // Filtrar por fecha
                DateTime fechaDesde;
                DateTime fechaHasta;

                if (DateTime.TryParse(txtFechaDesde.Text, out fechaDesde))
                {
                    todosMovimientos = todosMovimientos.Where(m => m.Fecha >= fechaDesde).ToList();
                }

                if (DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
                {
                    // Asegurar que la fecha hasta incluya todo el día
                    fechaHasta = fechaHasta.AddDays(1).AddSeconds(-1);
                    todosMovimientos = todosMovimientos.Where(m => m.Fecha <= fechaHasta).ToList();
                }

                // Paginación
                const int itemsPorPagina = 10;
                TotalPaginas = (int)Math.Ceiling((double)todosMovimientos.Count / itemsPorPagina);

                Movimientos = todosMovimientos.Skip(PaginaActual * itemsPorPagina).Take(itemsPorPagina).ToList();

                // Actualizar los estados de los botones
                btnAnterior.Enabled = PaginaActual > 0;
                btnSiguiente.Enabled = PaginaActual < TotalPaginas - 1;

                // Actualizar la UI
                DataBind();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            // Verifica si la página actual es mayor a 0 antes de retroceder
            if (PaginaActual > 0)
            {
                PaginaActual--;
                CargarBitacora();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (PaginaActual < TotalPaginas - 1)
            {
                PaginaActual++;
                CargarBitacora();
            }
        }

        protected void FiltrarBitacora(object sender, EventArgs e)
        {
            PaginaActual = 0; // Reiniciar a la primera página cuando se aplica un filtro
            CargarBitacora();
        }
    }
}