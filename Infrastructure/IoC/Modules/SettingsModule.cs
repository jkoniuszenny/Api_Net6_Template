
using Autofac;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Shared.Settings;

namespace Infrastructure.IoC.Modules;

public class SettingsModule : Autofac.Module
{
    private readonly IConfiguration _configuration;

    public SettingsModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterInstance(_configuration.GetSettings<DatabaseSettings>())
                .SingleInstance();
        builder.RegisterInstance(_configuration.GetSettings<RedisSettings>())
                .SingleInstance();
        builder.RegisterInstance(_configuration.GetSettings<ResponseTimeSettings>())
                .SingleInstance();
        builder.RegisterInstance(_configuration.GetSettings<TokenSettings>())
                .SingleInstance();
    }
}

