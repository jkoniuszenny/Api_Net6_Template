using Application.CQRS.Sample.Queries.GetAll;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Endpoints.Sample;

public class SampleOne : FastEndpoint
{
    public SampleOne()
    {
        Method = HttpRequestMethodTypes.Get;
        Url = "/sampleget";
        Name = "One";
        Tag = "Sample";
    }


    public async Task<JsonResult> ExecuteAsync([FromServices] IMediator mediator)
    {
        return new JsonResult(await mediator.Send(new GetAllSampleQuery()
        {
            Id = 44
        }));
    }

}

