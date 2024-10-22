using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaAcciones : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;

        public AltaAcciones()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

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