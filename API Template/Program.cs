using Api.Middlewares;
using Shared.Settings;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints.Extensions;
using Infrastructure.IoC;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Config;
using NLog.Web;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

IWebHostEnvironment environment = builder.Environment;
ConfigurationManager configuration = builder.Configuration;

configuration.SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ContainerModule(configuration));
});

builder.Host.UseNLog();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Token:IssuerKey"],
        ValidAudience = configuration["Token:AudienceKey"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecretKey"]))
    };
});

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, Array.Empty<string>()}
    });
});

builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();

app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
    });

app.UseHttpsRedirection();
app.ConfigureExceptionHandler();

var responsetimesettings = app.Services.GetService<ResponseTimeSettings>();
if (responsetimesettings?.Enabled ?? false)
{
    app.ConfigureResponseTime();
}

app.ConfigureBuffer();

app.UseAuthorization();
app.UseAuthentication();

app.UseMinimalEndpoints("EndpointsController");

var configuringFileName = $"nlog{(!environment.IsProduction() ? $".{environment.EnvironmentName.ToLower()}" : string.Empty)}.config";
var logger = NLogBuilder.ConfigureNLog(configuringFileName).Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
LogManager.Configuration.Install(new InstallationContext());

try
{
    logger.Debug("Starting program");
    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

