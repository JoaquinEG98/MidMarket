using MidMarket.Business.Interfaces;
using MidMarket.Entities;
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
    public partial class ModificarBono : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;
        public Bono Bono { get; set; }
        private int _bonoId { get; set; }

        public ModificarBono()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _bonoId = int.Parse(Request.QueryString["id"]);

                CargarBono();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}");
                Response.Redirect("AdministrarBonos.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreBono = Request.Form["nombreBono"];
                decimal valorNominal = decimal.Parse(Request.Form["valorNominal"]);
                float tasaInteres = float.Parse(Request.Form["tasaInteres"]);

                GuardarBono(nombreBono, valorNominal, tasaInteres);
                AlertHelper.MostrarMensaje(this, $"Bono {nombreBono} modificado correctamente.");
                CargarBono();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al modificar el bono: {ex.Message}");
            }
        }

        private void GuardarBono(string nombreBono, decimal valorNominal, float tasaInteres)
        {
            var bono = new Bono()
            {
                Id = Bono.Id,
                Id_Bono = Bono.Id_Bono,
                Nombre = nombreBono,
                ValorNominal = valorNominal,
                TasaInteres = tasaInteres
            };

            _activoService.ModificarBono(bono);
        }

        private void CargarBono()
        {
            Bono = _activoService.GetBonos().FirstOrDefault(x => x.Id == _bonoId);
            if (Bono == null)
            {
                AlertHelper.MostrarMensaje(this, "Bono no encontrado.");
                Response.Redirect("AdministrarBonos.aspx");
            }
        }
    }
}