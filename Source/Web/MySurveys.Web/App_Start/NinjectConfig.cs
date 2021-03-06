[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MySurveys.Web.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MySurveys.Web.App_Start.NinjectConfig), "Stop")]

namespace MySurveys.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using Data;
    using Data.Repository;
    using Infrastructure.Caching;
    using Infrastructure.IdBinder;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using Services.Contracts;

    public static class NinjectConfig
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<MySurveysDbContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(DeletableEntityRepository<>));
            kernel.Bind<ICacheService>().To<HttpCacheService>().InRequestScope();
            kernel.Bind<IIdentifierProvider>().To<IdentifierProvider>().InRequestScope();

            kernel.Bind(k => k.FromAssemblyContaining<IService>()
                                .SelectAllClasses()
                                .BindDefaultInterfaces()
                                .Configure(b => b.InRequestScope()));
        }
    }
}
