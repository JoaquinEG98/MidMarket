using MidMarket.Business;
using MidMarket.Business.Interfaces;
using MidMarket.Business.Services;
using MidMarket.DataAccess.DAOs;
using MidMarket.DataAccess.Interfaces;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Injection;

namespace MidMarket.UI
{
    public class Global : HttpApplication
    {
        public static IUnityContainer Container { get; private set; }

        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterDependencies();
        }

        private static void RegisterDependencies()
        {
            Container = new UnityContainer();

            Container.RegisterType<ISessionManager, SessionManager>();

            Container.RegisterType<IUsuarioService, UsuarioService>();
            Container.RegisterType<IPermisoService, PermisoService>();
            Container.RegisterType<IDigitoVerificadorService, DigitoVerificadorService>();
            Container.RegisterType<IBitacoraService, BitacoraService>();
            Container.RegisterType<IBackupService, BackupService>();
        }
    }
}