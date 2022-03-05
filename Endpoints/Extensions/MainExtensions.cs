namespace Endpoints.Extensions;

using System.Linq.Expressions;
using Configuration;
using Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public static class MainExtensions
{
    public static WebApplication UseMinimalEndpoints(this WebApplication app, string projectName)
    {
        var endpoints = AppDomain.CurrentDomain.GetAssemblies()
            .Where(w => w.FullName.Contains(projectName))
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IFastEndpoint).IsAssignableFrom(p) && !p.Name.Contains("FastEndpoint"));

        foreach (var item in endpoints)
        {
            IFastEndpoint? endpointClassInit = Activator.CreateInstance(item) as IFastEndpoint;

            var requestExecuteMethod = item?.GetMethod("ExecuteAsync");

            if (requestExecuteMethod is null || endpointClassInit is null)
                continue;

            var dynamicDelegateForRequestExecuteMethod = Delegate.CreateDelegate(
                    Expression.GetDelegateType(
                        requestExecuteMethod.GetParameters()
                            .Select(p => p.ParameterType)
                            .Concat(new Type[] { requestExecuteMethod.ReturnType })
                            .ToArray()),
                    endpointClassInit,
                    requestExecuteMethod);

            switch (endpointClassInit?.Method)
            {
                case HttpRequestMethodTypes.Get:
                    app.MapGet(endpointClassInit.Url ?? string.Empty, dynamicDelegateForRequestExecuteMethod)
                       .Produces<IDictionary<string, string>>(StatusCodes.Status200OK)
                       .WithName(endpointClassInit.Name ?? string.Empty)
                       .WithTags(endpointClassInit.Tag ?? string.Empty);
                    break;
                case HttpRequestMethodTypes.Post:
                    app.MapPost(endpointClassInit.Url ?? string.Empty, dynamicDelegateForRequestExecuteMethod)
                       .Produces(StatusCodes.Status200OK)
                       .WithName(endpointClassInit.Name ?? String.Empty)
                       .WithTags(endpointClassInit.Tag ?? String.Empty);
                    break;
                case HttpRequestMethodTypes.Put:
                    app.MapPut(endpointClassInit.Url ?? string.Empty, dynamicDelegateForRequestExecuteMethod)
                       .Produces(StatusCodes.Status200OK)
                       .WithName(endpointClassInit.Name ?? string.Empty)
                       .WithTags(endpointClassInit.Tag ?? string.Empty);
                    break;
                case HttpRequestMethodTypes.Delete:
                    app.MapDelete(endpointClassInit.Url ?? string.Empty, dynamicDelegateForRequestExecuteMethod)
                       .Produces(StatusCodes.Status200OK)
                       .WithName(endpointClassInit.Name ?? string.Empty)
                       .WithTags(endpointClassInit.Tag ?? string.Empty);
                    break;
                default:
                    break;

            }
        }

        return app;
    }
}
