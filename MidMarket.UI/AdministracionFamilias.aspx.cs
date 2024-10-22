using MidMarket.Business.Interfaces;
using MidMarket.Entities.Composite;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using Unity;

namespace MidMarket.UI
{
    public partial class AdministracionFamilias : System.Web.UI.Page
    {
        private readonly IPermisoService _permisoService;
        public IList<Familia> Familias { get; set; }

        public AdministracionFamilias()
        {
            _permisoService = Global.Container.Resolve<IPermisoService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Familias = _permisoService.GetFamilias();
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"Error al cargar la página: {ex.Message}.");
            }
        }
    }
}