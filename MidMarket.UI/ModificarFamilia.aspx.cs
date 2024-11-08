using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;

namespace MidMarket.UI
{
    public partial class ModificarFamilia : System.Web.UI.Page
    {
        private readonly IPermisoService _permisoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public Familia Familia { get; set; }
        public IList<Componente> Patentes { get; set; }
        public IList<Patente> PatentesExistentes { get; set; }
        private int _familiaId { get; set; }

        public ModificarFamilia()
        {
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.ModificarFamilia))
                Response.Redirect("Default.aspx");

            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                _familiaId = int.Parse(Request.QueryString["id"]);
                CargarFamiliasPatentes();
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            try
            {
                string nombreFamilia = Request.Form["nombreFamilia"];
                string patentesSeleccionadas = Request.Form["patentesSeleccionadas"];

                string[] patentesIds = patentesSeleccionadas.Split(',');

                if (!string.IsNullOrEmpty(nombreFamilia) && patentesIds.Length > 0)
                {
                    GuardarFamilia(nombreFamilia, patentesIds);
                    CargarFamiliasPatentes();

                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_29")} {nombreFamilia}");
                }
                else
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_30")} {nombreFamilia}");
                }
            }
            catch (SqlException)
            {
                AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "ERR_03")}");
            }
            catch (Exception ex)
            {
                AlertHelper.MostrarModal(this, $"{ex.Message}.");
            }
        }

        private void GuardarFamilia(string nombreFamilia, string[] patentesIds)
        {
            var familia = new Familia()
            {
                Id = _familiaId,
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

            _permisoService.GuardarFamiliaCreada(familia);
        }

        private void CargarFamiliasPatentes()
        {
            Familia = _permisoService.GetFamilias().FirstOrDefault(x => x.Id == _familiaId);
            Patentes = _permisoService.TraerFamiliaPatentes(_familiaId);

            PatentesExistentes = _permisoService.GetPatentes();
            PatentesExistentes = PatentesExistentes.Where(p => !Patentes.Any(pa => pa.Id == p.Id)).ToList();
        }
    }
}