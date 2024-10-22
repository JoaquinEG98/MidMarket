using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Unity;

namespace MidMarket.UI
{
    public partial class Compra : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        public IList<Bono> Bonos { get; set; }
        public IList<Accion> Acciones { get; set; }

        public Compra()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Bonos = _activoService.GetBonos();
                Acciones = _activoService.GetAcciones();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
            }
        }
    }
}