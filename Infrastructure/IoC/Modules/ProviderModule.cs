using Application.Interfaces.Providers;
using Autofac;
using System.Reflection;

namespace Infrastructure.IoC.Modules;

public class ProviderModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(ProviderModule)
            .GetTypeInfo()
            .Assembly;

        builder.RegisterAssemblyTypes(assembly)
               .Where(x => x.IsAssignableTo<IProvider>())
               .AsImplementedInterfaces()
               .InstancePerDependency();
    }
}

