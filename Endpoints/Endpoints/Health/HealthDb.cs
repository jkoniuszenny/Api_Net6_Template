using Application.CQRS.DatabaseHealth.Queries.CheckDatabaseHealth;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EndpointsController.Endpoints.Health;

public class HealthDb : FastEndpoint
{
    public HealthDb()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/Health/Db";
        Name = "Db";
        Tag = "Health";
    }


    public async Task<IResult> ExecuteAsync(IMediator mediator)
    {
        return Results.Ok(await mediator.Send(new CheckDatabaseHealthQuery()));
    }

}

