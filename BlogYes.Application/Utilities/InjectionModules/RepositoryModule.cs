using Autofac;
using BlogYes.Infrastructure.DbContexts;
using System.Reflection;

namespace BlogYes.Application.Utilities.InjectionModules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("BlogYes." + nameof(Infrastructure)))
                .Where(type => type.IsInNamespace("BlogYes.Infrastructure.Repositories"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();
            var types = Assembly.Load("BlogYes." + nameof(Infrastructure)).GetTypes().Where(type => type.IsInNamespace("BlogYes.Infrastructure.Repositories"));
        }
    }
}
