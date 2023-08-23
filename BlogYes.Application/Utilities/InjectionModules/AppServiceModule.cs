using Autofac;
using BlogYes.Application.Services.Base;
using BlogYes.Domain.Services;
using System.Reflection;

namespace BlogYes.WebApi.Utilities.InjectionModules
{
    public class AppServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("BlogYes." + nameof(Application)))
                .Where(type => type.IsAssignableTo(typeof(IBaseService)))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.Load("BlogYes." + nameof(Domain)), Assembly.Load("BlogYes." + nameof(Infrastructure)))
                .Where(type => type.IsAssignableTo<IDomainService>())
                .AsImplementedInterfaces()
                .PropertiesAutowired();
        }
    }
}