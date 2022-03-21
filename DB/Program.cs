using Application.Interfaces.Repositories;
using Autofac;
using Infrastructure.IoC;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables();

var configuration = builder.Build();

var autoBuilder = new ContainerBuilder();
autoBuilder.RegisterModule(new ContainerModule(configuration));

var applicationContainer = autoBuilder.Build();

var scope = applicationContainer.BeginLifetimeScope();

var plugin = scope.Resolve<IMigrateRepository>();

await plugin.MigrateExecute();
