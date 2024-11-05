using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using MidMarket.UI.Helpers;
using System;
using System.Linq;
using Unity;

namespace MidMarket.UI
{
    public partial class Default : System.Web.UI.Page, IObserver
    {
        private readonly ISessionManager _sessionManager;
        private readonly ITraduccionService _traduccionService;

        public Default()
        {
            _sessionManager = Global.Container.Resolve<ISessionManager>();
            _traduccionService = Global.Container.Resolve<ITraduccionService>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cliente = _sessionManager.Get<Cliente>("Usuario");

                if (cliente != null)
                {
                    Response.Redirect("MenuPrincipal.aspx");
                }
                else
                {
                    CargarIdiomas();
                    SuscribirClienteAIdioma();
                }
            }
            else
            {
                string eventTarget = Request["__EVENTTARGET"];
                string eventArgument = Request["__EVENTARGUMENT"];

                if (eventTarget == "ChangeLanguage" && int.TryParse(eventArgument, out int idiomaId))
                {
                    CambiarIdioma(idiomaId);
                }
            }

            VerificarIdioma();
        }

        private void CargarIdiomas()
        {
            var idiomas = _traduccionService.ObtenerIdiomas();
            idiomaRepeater.DataSource = idiomas;
            idiomaRepeater.DataBind();
        }

        private void SuscribirClienteAIdioma()
        {
            var cliente = _sessionManager.Get<Cliente>("Usuario");

            if (cliente != null && !_sessionManager.IsObserverSubscribed())
            {
                cliente.SuscribirObservador(this);
                _sessionManager.ObserverSubscribe();
            }
        }

        private void CambiarIdioma(int idiomaId)
        {
            var idioma = _traduccionService.ObtenerIdiomas().FirstOrDefault(x => x.Id == idiomaId);
            if (idioma != null)
            {
                _sessionManager.Set("Idioma", idioma);
                UpdateLanguage(idioma);
            }
        }

        private void VerificarIdioma()
        {
            var idioma = _sessionManager.Get<IIdioma>("Idioma");

            UpdateLanguage(idioma);
        }

        public void UpdateLanguage(IIdioma idioma)
        {
            var traducciones = _traduccionService.ObtenerTraducciones(idioma);
            ScriptHelper.TraducirPagina(this.Page, traducciones, _sessionManager);
        }
    }
}
