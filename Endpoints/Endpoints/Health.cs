using Application.CQRS.Sample.Queries.GetAll;
using Application.Interfaces.Services;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints;

public class Health : FastEndpoint
{
    public Health()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/query";
        Name = "OOOO";
        Tag = "Mediator";
    }


    public async Task<JsonResult> ExecuteAsync([FromServices] IMediator mediator)
    {
        return new JsonResult(await mediator.Send(new GetAllSampleQuery()
        {
            Id = 44
        }));
    }

}

