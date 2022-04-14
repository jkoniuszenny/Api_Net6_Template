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
        builder.RegisterModule(new DbContextModule(_configuration));
        builder.RegisterModule<AutoMapperModule>();
        builder.RegisterModule<MediatrModule>();
        builder.RegisterModule<RepositoryModule>();
        builder.RegisterModule<ServiceModule>();
        builder.RegisterModule<ProviderModule>();
        builder.RegisterModule<NLogModule>();
        builder.RegisterModule(new SettingsModule(_configuration));
    }
}

