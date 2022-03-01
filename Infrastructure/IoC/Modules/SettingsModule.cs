using Application.Settings;
using Autofac;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.IoC.Modules
{
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
            builder.RegisterInstance(_configuration.GetSettings<ResponseTimeSettings>())
                    .SingleInstance();
        }
    }
}
