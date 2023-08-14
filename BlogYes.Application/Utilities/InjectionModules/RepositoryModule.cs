using Autofac;
using BlogYes.Domain.Repositories;
using System.Reflection;

namespace BlogYes.Application.Utilities.InjectionModules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Infrastructure)))
                .Where(type => type.IsAssignableTo(typeof(IRepository<,>)))
                .AsImplementedInterfaces();
        }
    }
}
