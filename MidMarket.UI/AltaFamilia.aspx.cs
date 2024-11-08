using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Composite;
using MidMarket.Entities.Observer;
using MidMarket.Seguridad;
using MidMarket.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Unity;

namespace MidMarket.UI
{
    public partial class AltaFamilia : System.Web.UI.Page
    {
        private readonly IPermisoService _permisoService;
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public IList<Patente> Patentes { get; set; }

        public AltaFamilia()
        {
            _permisoService = Global.Container.Resolve<IPermisoService>();
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var clienteLogueado = _sessionManager.Get<Cliente>("Usuario");
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            if (clienteLogueado == null || !PermisoCheck.VerificarPermiso(clienteLogueado.Permisos, Entities.Enums.Permiso.AltaFamilia))
                Response.Redirect("Default.aspx");

            try
            {
                Patentes = _permisoService.GetPatentes();
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

        protected void btnCrear_Click(object sender, EventArgs e)
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

                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_08")}");
                }
                else
                {
                    AlertHelper.MostrarModal(this, $"{_traduccionService.ObtenerMensaje(idioma, "MSJ_09")} {nombreFamilia}.");
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