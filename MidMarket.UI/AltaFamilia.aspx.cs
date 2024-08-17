using MidMarket.Business.Interfaces;
using MidMarket.Entities.Composite;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaFamilia : System.Web.UI.Page
    {
        private readonly IPermisoService _permisoService;
        public IList<Patente> Patentes { get; set; }

        public AltaFamilia()
        {
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Patentes = _permisoService.GetPatentes();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al cargar la página: {ex.Message}.");
            }

        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreFamilia = Request.Form["nombreFamilia"];
                string patentesSeleccionadas = Request.Form["patentesSeleccionadas"];

                string[] patentesIds = patentesSeleccionadas.Split(',');

                if (!string.IsNullOrEmpty(nombreFamilia) && patentesIds.Length > 0)
                {
                    GuardarFamilia(nombreFamilia, patentesIds);

                    AlertHelper.MostrarMensaje(this, $"Familia {nombreFamilia} creada correctamente.");
                }
                else
                {
                    AlertHelper.MostrarMensaje(this, $"Error al querer crear la familia: {nombreFamilia}.");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarMensaje(this, $"Error al querer crear la familia: {ex.Message}.");
            }

        }

        private void GuardarFamilia(string nombreFamilia, string[] patentesIds)
        {
            var familia = new Familia()
            {
                Nombre = nombreFamilia,
            };

            foreach (string id in patentesIds)
            {
                var patente = new Patente()
                {
                    Id = int.Parse(id),
                };

                familia.AgregarHijo(patente);
            }

            int familiaId = _permisoService.GuardarPatenteFamilia(familia, true);

            if (familiaId > 0)
            {
                familia.Id = familiaId;
                _permisoService.GuardarFamiliaCreada(familia);
            }
        }
    }
}