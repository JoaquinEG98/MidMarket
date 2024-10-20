using MidMarket.Business.Interfaces;
using MidMarket.Business.Services;
using MidMarket.DataAccess.DAOs;
using MidMarket.DataAccess.Interfaces;
using Unity;

namespace MidMarket.Business
{
    public static class DependencyResolver
    {
        public static IUnityContainer Container { get; set; }

        static DependencyResolver()
        {
            Container = new UnityContainer();

            Container.RegisterType<ISessionManager, SessionManager>();

            Container.RegisterType<IUsuarioDAO, UsuarioDAO>();
            Container.RegisterType<IPermisoDAO, PermisoDAO>();
            Container.RegisterType<IDigitoVerificadorDAO, DigitoVerificadorDAO>();
            Container.RegisterType<IBitacoraDAO, BitacoraDAO>();
            Container.RegisterType<IBackupDAO, BackupDAO>();
            Container.RegisterType<IActivoDAO, ActivoDAO>();

            Container.RegisterType<IPermisoService, PermisoService>();
            Container.RegisterType<IDigitoVerificadorService, DigitoVerificadorService>();
            Container.RegisterType<IUsuarioService, UsuarioService>();
            Container.RegisterType<IBitacoraService, BitacoraService>();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
