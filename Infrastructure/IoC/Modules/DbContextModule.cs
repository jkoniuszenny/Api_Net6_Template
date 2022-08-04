using Autofac;
using Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IoC.Modules;

public class DbContextModule : Module
{
    private readonly IConfiguration _configuration;

    public DbContextModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var connectionString = _configuration.GetSection("Database").GetSection("ConnectionString").Value;

        var optionBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionBuilder.UseSqlServer(connectionString);

        builder.RegisterType<DatabaseContext>()
           .WithParameter("options", optionBuilder.Options)
           .InstancePerLifetimeScope();

        builder.RegisterType<HttpContextAccessor>()
           .As<IHttpContextAccessor>()
           .SingleInstance();

        builder.RegisterType<DatabaseMongoContext>()
           .InstancePerLifetimeScope();
    }
}

