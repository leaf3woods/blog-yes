﻿using Autofac;
using BlogYes.Infrastructure.DbContexts;
using Microsoft.Extensions.Logging;

namespace BlogYes.Application.Utilities.InjectionModules
{
    public class SingletonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => new LoggerFactory().CreateLogger("Adapter"))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<InitialDatabase>()
                .SingleInstance();
        }
    }
}