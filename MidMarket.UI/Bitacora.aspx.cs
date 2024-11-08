using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using MidMarket.XML;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class Bitacora : System.Web.UI.Page
    {
        private readonly IBitacoraService _bitacoraService;
        private readonly IUsuarioService _usuarioService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public List<Entities.Bitacora> Movimientos { get; set; } = new List<Entities.Bitacora>();

        private bool _filtrado
        {
            get
            {
                return ViewState["Filtrado"] != null ? (bool)ViewState["Filtrado"] : false;
            }
            set
            {
                ViewState["Filtrado"] = value;
            }
        }

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
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

                if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.VisualizarBitacora))
                    Response.Redirect("Default.aspx");

                if (!IsPostBack)
                {
                    CargarClientes();
                    PaginaActual = 0;
                    ConsultarBitacora();
                }
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        private void CargarClientes()
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

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
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        private void ConsultarBitacora()
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                List<Entities.Bitacora> todosMovimientos = new List<Entities.Bitacora>();

                if (_filtrado)
                    todosMovimientos = _bitacoraService.ObtenerBitacora();
                else
                    todosMovimientos = _bitacoraService.ObtenerBitacora().Take(30).ToList();

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
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 0)
            {
                PaginaActual--;
                ConsultarBitacora();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (PaginaActual < TotalPaginas - 1)
            {
                PaginaActual++;
                ConsultarBitacora();
            }
        }

        protected void ConsultarBitacoraFiltro(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                ValidarFiltros();
                PaginaActual = 0;

                _filtrado = true;
                ConsultarBitacora();
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");

                PaginaActual = 0;
                TotalPaginas = 1;
                Movimientos = new List<Entities.Bitacora>();

                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;

                DataBind();
            }
        }

        protected void ValidarFiltros()
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            bool seleccionoFechas = !string.IsNullOrEmpty(txtFechaDesde.Text) && !string.IsNullOrEmpty(txtFechaHasta.Text);

            if (seleccionoFechas && Convert.ToDateTime(txtFechaDesde.Text) > Convert.ToDateTime(txtFechaHasta.Text))
            {
                throw new Exception($"{_traduccionService.ObtenerMensaje(idioma, "ERR_20")}");
            }
        }

        protected void ExportarXML_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");


            try
            {
                BitacoraXML.GenerarXMLBitacora(ObtenerMovimientosExportar());

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_13")}");

                ConsultarBitacora();
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        protected void ExportarExcel_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                BitacoraXML.GenerarExcelBitacora(ObtenerMovimientosExportar());

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_14")}");

                ConsultarBitacora();
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        protected void LimpiarBitacora_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                var limpiarBitacora = _bitacoraService.LimpiarBitacora();

                BitacoraXML.GenerarXMLLimpiarBitacora(limpiarBitacora);
                BitacoraXML.GenerarExcelLimpiarBitacora(limpiarBitacora);

                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_41")}");

                ConsultarBitacora();
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}");
            }
        }

        private List<Entities.Bitacora> ObtenerMovimientosExportar()
        {
            List<Entities.Bitacora> todosMovimientos = _bitacoraService.ObtenerBitacora();

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

            return todosMovimientos.OrderByDescending(m => m.Fecha).Take(50).ToList();
        }
    }
}