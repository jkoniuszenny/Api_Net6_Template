using Api.Middlewares;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints.Extensions;
using Infrastructure.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Config;
using NLog.Web;
using Shared.Settings;
using System.Text;

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

builder.Services.AddStackExchangeRedisCache(x =>
{
    x.Configuration = configuration["Redis:ConnectionString"];
});

//builder.Services.AddHttpContextAccessor();

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

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.DictionaryKeyPolicy = null;
    options.SerializerOptions.WriteIndented = true;
});

//builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Please insert JWT token into field"
    });
    c.IncludeXmlComments(PlatformServices.Default.Application.ApplicationBasePath + "\\api.xml");
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api ");

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

app.UseMinimalEndpoints(c => c.ProjectName = "EndpointsController");

var configuringFileName = $"nlog.config";
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