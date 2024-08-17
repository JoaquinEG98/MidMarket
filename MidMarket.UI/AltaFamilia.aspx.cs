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
            if (!IsPostBack)
            {
                Patentes = _permisoService.GetPatentes();
            }
        }
    }
}