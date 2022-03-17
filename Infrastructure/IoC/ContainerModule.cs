using Autofac;
using Infrastructure.IoC.Modules;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.IoC;

public class ContainerModule : Autofac.Module
{
    private readonly IConfiguration _configuration;

    public ContainerModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        // builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
        builder.RegisterModule<MediatrModule>();
        builder.RegisterModule<RepositoryModule>();
        builder.RegisterModule<ProviderModule>();
        builder.RegisterModule<NLogModule>();
        builder.RegisterModule(new SettingsModule(_configuration));
    }
}

