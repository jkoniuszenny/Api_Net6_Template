using Autofac;
using Shared.NLog;
using Shared.NLog.Interfaces;

namespace Infrastructure.IoC.Modules;

public class NLogModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<NLogLogger>()
                .As<INLogLogger>()
                .SingleInstance();

        builder.RegisterType<NLogTimeLogger>()
                .As<INLogTimeLogger>()
                .SingleInstance();
    }
}
