using Application.Interfaces.Services;
using Autofac;
using System.Reflection;

namespace Infrastructure.IoC.Modules;

public class ServiceModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(ServiceModule)
            .GetTypeInfo()
            .Assembly;

        builder.RegisterAssemblyTypes(assembly)
               .Where(x => x.IsAssignableTo<IServices>())
               .AsImplementedInterfaces()
               .InstancePerDependency();
    }
}

