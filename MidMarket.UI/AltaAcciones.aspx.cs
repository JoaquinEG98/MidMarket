using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaAcciones : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        public IList<Patente> Patentes { get; set; }

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

                AlertHelper.MostrarMensaje(this, $"Acción {accion.Nombre} dada de alta correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al dar de alta la Acción: {ex.Message}.");
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