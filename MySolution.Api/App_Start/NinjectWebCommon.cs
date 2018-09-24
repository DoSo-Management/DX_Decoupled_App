using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MySolution.Api.App_Start;
using Ninject;
using Ninject.Web.Common;
using RepoServices;
using System;
using System.Web;
using BLL;
using ApiViewModelMapper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]


namespace MySolution.Api.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IApiMapper>().To<ApiMapper>();
            kernel.Bind<IPolicyBl>().To<PolicyBl>();
            kernel.Bind(typeof(IRepositoryID<>)).To(typeof(DBRepository<>));

            //foreach (var type in typeof(DBRepository<>).Assembly.GetTypes().Where(x => x.IsClass))
            //{
            //    foreach (var @interface in type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepositoryID<>)))
            //        kernel.Bind(@interface).To(type);
            //}
        }
    }
}