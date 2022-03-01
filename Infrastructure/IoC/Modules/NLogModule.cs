using Application.NLog;
using Application.NLog.Interfaces;
using Autofac;


namespace Infrastructure.IoC.Modules
{
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
}
