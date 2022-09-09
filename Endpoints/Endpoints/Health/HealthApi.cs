using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using Microsoft.AspNetCore.Http;

namespace EndpointsController.Endpoints.Health;

public class HealthApi : FastEndpoint
{
    public HealthApi()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/Health/Api";
        Name = "Api";
        Tag = "Health";
    }

    public async Task<IResult> ExecuteAsync()
    {
        var communicate = await Task.FromResult($"I'm alive - Api - {AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(f => f.GetName().Name == "Api")?.GetName()?.Version }");

        return Results.Ok(communicate);
    }

}

