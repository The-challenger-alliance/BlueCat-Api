using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BlueCat.Api.Repository.Impl.Context;
using BlueCat.Api.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BlueCat.Api.App_Start
{
    public class AutoFacConfig
    {
        public static void Register()
        {
            // code from http://www.cnblogs.com/yinrq/p/5383396.html

            var builder = new ContainerBuilder();
            HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;
         
            //注册所有的ApiControllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            AutoFacConfig.SetUpResolveRules(builder);


            builder.RegisterType<BlueCatDbContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterType<BlueCatUnitOfWorkContext>().As<IUnitOfWork>().InstancePerLifetimeScope();


            var container = builder.Build();
            //注册api容器需要使用HttpConfiguration对象
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        private static void SetUpResolveRules(ContainerBuilder containerBuilder)
        {
            //WebAPI只用引用services和repository的接口，不用引用实现的dll。
            //如需加载实现的程序集，将dll拷贝到bin目录下即可，不用引用dll
            
            var iService = Assembly.Load("BlueCat.Api.Service.Interface");
            var service = Assembly.Load("BlueCat.Api.Service.Impl");
            var iRepository = Assembly.Load("BlueCat.Api.Repository.Interface");
            var repository = Assembly.Load("BlueCat.Api.Repository.Impl");

            var unitOfWork = Assembly.Load("BlueCat.Api.UnitOfWork");

            containerBuilder.RegisterAssemblyTypes(unitOfWork, repository)
.Where(t => t.Name.Contains("UnitOfWork"))
.AsImplementedInterfaces();

            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            containerBuilder.RegisterAssemblyTypes(iService, service)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            containerBuilder.RegisterAssemblyTypes(iRepository, repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces();





           // containerBuilder.RegisterAssemblyTypes(iRepository, repository)
           //.Where(t => t.Name.EndsWith("Repository"))
           //.AsImplementedInterfaces();

        }
    }
}