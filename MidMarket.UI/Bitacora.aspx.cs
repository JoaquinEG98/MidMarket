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

        protected int GetPaginaActual() => PaginaActual + 1;
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
                CargarClientes();
                PaginaActual = 0;
                CargarBitacora();
            }
        }

        private void CargarClientes()
        {
            try
            {
                var clientes = _usuarioService.GetClientes();
                ddlUsuario.Items.Clear();
                ddlUsuario.Items.Add(new ListItem("Todos", ""));

                foreach (var cliente in clientes)
                {
                    ddlUsuario.Items.Add(new ListItem(cliente.RazonSocial, cliente.RazonSocial));
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

                if (!string.IsNullOrEmpty(ddlUsuario.SelectedValue))
                {
                    todosMovimientos = todosMovimientos.Where(m => m.Cliente.RazonSocial == ddlUsuario.SelectedValue).ToList();
                }

                if (!string.IsNullOrEmpty(ddlCriticidad.SelectedValue))
                {
                    todosMovimientos = todosMovimientos.Where(m => m.Criticidad.ToString() == ddlCriticidad.SelectedValue).ToList();
                }

                DateTime fechaDesde;
                DateTime fechaHasta;

                if (DateTime.TryParse(txtFechaDesde.Text, out fechaDesde))
                {
                    todosMovimientos = todosMovimientos.Where(m => m.Fecha >= fechaDesde).ToList();
                }

                if (DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
                {
                    fechaHasta = fechaHasta.AddDays(1).AddSeconds(-1);
                    todosMovimientos = todosMovimientos.Where(m => m.Fecha <= fechaHasta).ToList();
                }

                const int itemsPorPagina = 10;
                TotalPaginas = (int)Math.Ceiling((double)todosMovimientos.Count / itemsPorPagina);

                Movimientos = todosMovimientos.Skip(PaginaActual * itemsPorPagina).Take(itemsPorPagina).ToList();

                btnAnterior.Enabled = PaginaActual > 0;
                btnSiguiente.Enabled = PaginaActual < TotalPaginas - 1;

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
            PaginaActual = 0;
            CargarBitacora();
        }
    }
}