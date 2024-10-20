using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using Unity;

namespace MidMarket.UI
{
    public partial class AdministrarAcciones : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        public IList<Accion> Acciones { get; set; }

        public AdministrarAcciones()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Acciones = _activoService.GetAcciones();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
            }
        }
    }
}