using Application.CQRS.Sample.Queries.GetAll;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints.Health;

public class HealthApi : FastEndpoint
{
    public HealthApi()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/Health/Api";
        Name = "Api";
        Tag = "Health";
    }


    public async Task<string> ExecuteAsync()
    {
        return await Task.FromResult($"I'm alive - Api - {AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(f=>f.GetName().Name == "Api")?.GetName()?.Version }");
    }

}

