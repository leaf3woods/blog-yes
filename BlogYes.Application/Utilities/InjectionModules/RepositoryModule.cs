using Autofac;
using System.Reflection;

namespace BlogYes.WebApi.Utilities.InjectionModules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("BlogYes." + nameof(Infrastructure)))
                .Where(type => type.IsInNamespace("BlogYes.Infrastructure.Repositories"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();
        }
    }
}
