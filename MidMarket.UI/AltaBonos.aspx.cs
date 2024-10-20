using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.UI.Helpers;
using System;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaBonos : System.Web.UI.Page
    {
        private readonly IActivoService _activoService;

        public AltaBonos()
        {
            _activoService = Global.Container.Resolve<IActivoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCargarBono_Click(object sender, EventArgs e)
        {
            try
            {
                Bono bono = new Bono
                {
                    Nombre = nombreBono.Value,
                    ValorNominal = decimal.Parse(valorNominal.Value),
                    TasaInteres = float.Parse(tasaInteres.Value)
                };
                _activoService.AltaBono(bono);

                AlertHelper.MostrarMensaje(this, $"Bono {bono.Nombre} dado de alta correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al dar de alta la Acción: {ex.Message}.");
            }
        }


        private void LimpiarCampos()
        {
            nombreBono.Value = "";
            valorNominal.Value = "";
            tasaInteres.Value = "";
        }
    }
}