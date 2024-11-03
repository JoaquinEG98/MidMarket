using MidMarket.Entities.Observer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace MidMarket.UI.Helpers
{
    public static class ScriptHelper
    {
        public static void TraducirPagina(Page page, IDictionary<string, ITraduccion> traducciones)
        {
            var traduccionesTextos = traducciones.ToDictionary(t => t.Key, t => t.Value.Texto);
            var traduccionesJson = JsonConvert.SerializeObject(traduccionesTextos);

            page.ClientScript.RegisterStartupScript(
                page.GetType(),
                "SetTranslations",
                $"var traducciones = {traduccionesJson};",
                true
            );
        }
    }
}