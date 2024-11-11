using MidMarket.Business.Interfaces;
using MidMarket.Entities;
using MidMarket.Entities.Observer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace MidMarket.UI.Helpers
{
    public static class ScriptHelper
    {
        public static void TraducirPagina(Page page, IDictionary<string, ITraduccion> traducciones, ISessionManager sessionManager)
        {
            TraducirNombres(sessionManager, traducciones);

            if (traducciones != null)
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

        private static void TraducirNombres(ISessionManager sessionManager, IDictionary<string, ITraduccion> traducciones)
        {
            var cliente = sessionManager.Get<Cliente>("Usuario");

            if (cliente != null && traducciones != null && traducciones.ContainsKey("texto_Hola"))
            {
                string familia = string.Empty;

                var permisoFamilia = cliente.Permisos.FirstOrDefault(x => x.Permiso == Entities.Enums.Permiso.EsFamilia);
                if (permisoFamilia != null)
                {
                    familia = permisoFamilia.Nombre.ToString();
                }

                var saludo = traducciones["texto_Hola"].Texto;
                saludo = saludo.Replace("{RazonSocial}", cliente.RazonSocial)
                               .Replace("{Familia}", familia ?? "");
                traducciones["texto_Hola"].Texto = saludo;
            }
        }
    }
}