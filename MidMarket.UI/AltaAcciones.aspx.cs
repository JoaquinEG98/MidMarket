using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaAcciones : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        private readonly ISessionManager _sessionManager;

        public AltaAcciones()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AltaAccion))
                Response.Redirect("Default.aspx");
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Accion accion = new Accion
                {
                    Nombre = nombreAccion.Value,
                    Simbolo = simboloAccion.Value,
                    Precio = decimal.Parse(precioAccion.Value)
                };

                _activoService.AltaAccion(accion);

                AlertHelper.MostrarModal(this, $"Acción {accion.Nombre} dada de alta correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al dar de alta la Acción: {ex.Message}.");
            }
        }

        private void LimpiarCampos()
        {
            nombreAccion.Value = "";
            simboloAccion.Value = "";
            precioAccion.Value = "";
        }
    }
}