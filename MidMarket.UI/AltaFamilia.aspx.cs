using MidMarket.Business.Interfaces;
using MidMarket.Business.Services;
using MidMarket.Entities.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            //if (!IsPostBack)
            //{
                Patentes = _permisoService.GetPatentes();
            //}
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            // Obtener el nombre de la familia
            string nombreFamilia = Request.Form["nombreFamilia"];

            // Obtener los IDs de las patentes seleccionadas
            string patentesSeleccionadas = Request.Form["patentesSeleccionadas"];

            // Convertir los IDs a un array si lo necesitas
            string[] patentesIds = patentesSeleccionadas.Split(',');

            // Aquí puedes implementar la lógica para guardar en la base de datos o hacer otras operaciones
            if (!string.IsNullOrEmpty(nombreFamilia) && patentesIds.Length > 0)
            {
                // Ejemplo de guardado en la base de datos (puedes reemplazarlo con tu lógica real)
                GuardarFamilia(nombreFamilia, patentesIds);
                Response.Write($"Familia '{nombreFamilia}' creada con éxito con {patentesIds.Length} patentes.");
            }
            else
            {
                Response.Write("Error: Por favor, asegúrate de ingresar el nombre de la familia y seleccionar al menos una patente.");
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