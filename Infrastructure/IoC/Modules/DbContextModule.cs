using Autofac;
using Infrastructure.Database;

namespace Infrastructure.IoC.Modules;

public class DbContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DatabaseContext>()
           .AsImplementedInterfaces()
           .InstancePerMatchingLifetimeScope();

    }
}

