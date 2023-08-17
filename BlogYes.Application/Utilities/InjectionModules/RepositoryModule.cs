using Autofac;
using System.Reflection;

namespace BlogYes.WebApi.Utilities.InjectionModules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load(WithPrefix(nameof(Infrastructure))), Assembly.Load(WithPrefix(nameof(Domain))))
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();
        }

        private string WithPrefix(string content) => $"BlogYes." + content;
    }
}
