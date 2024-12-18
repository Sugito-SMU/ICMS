using Autofac;
using Autofac.Integration.WebApi;
using ICMS.API.Mappings;
using ICMS.Data.Infrastructure;
using ICMS.Data.Repositories;
using ICMS.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ICMS.API.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            //Configure AutoFac
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(ActivityRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            // Services
            builder.RegisterAssemblyTypes(typeof(ActivityManager).Assembly)
               .Where(t => t.Name.EndsWith("Manager"))
               .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}