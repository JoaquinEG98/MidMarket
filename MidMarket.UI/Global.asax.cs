using MidMarket.Business;
using MidMarket.Business.Interfaces;
using MidMarket.Business.Services;
using MidMarket.DataAccess.DAOs;
using MidMarket.DataAccess.Interfaces;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;

namespace MidMarket.UI
{
    public class Global : HttpApplication
    {
        public static IUnityContainer Container { get; private set; }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterDependencies();
        }

        private static void RegisterDependencies()
        {
            Container = new UnityContainer();

            Container.RegisterType<IUsuarioDAO, UsuarioDAO>();
            Container.RegisterType<IUsuarioService, UsuarioService>();
        }
    }
}