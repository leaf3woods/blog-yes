using Autofac;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BlogYes.Application.Utilities.InjectionModules
{
    public class ControllerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("BlogYes." + nameof(Application)))
                .Where(a => a.IsAssignableTo(typeof(ControllerBase)));
            var types = Assembly.Load("BlogYes." + nameof(Application)).GetTypes().Where(type => type.IsAssignableTo(typeof(ControllerBase)));

        }
    }
}
