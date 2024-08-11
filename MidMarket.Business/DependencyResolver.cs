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

            Container.RegisterType<IUsuarioDAO, UsuarioDAO>();
            Container.RegisterType<IUsuarioService, UsuarioService>();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
