using Application.CQRS.DatabaseHealth.Queries.Check;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints.Health;

public class HealthDb : FastEndpoint
{
    public HealthDb()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/Health/Db";
        Name = "Db";
        Tag = "Health";
    }


    public async Task<JsonResult> ExecuteAsync([FromServices] IMediator mediator)
    {
        return new JsonResult(await mediator.Send(new CheckDatabaseHealthQuery()));
    }

}

